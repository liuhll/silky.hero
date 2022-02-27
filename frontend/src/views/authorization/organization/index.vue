<template>
  <PageWrapper v-loading="loadingRef" loading-tip="加载中...">
    <Row :gutter="[24, 16]">
      <Col :span="6">
        <Card title="组织机构树">
          <template #extra>
            <a-button
              type="primary"
              v-auth="'Organization.Create'"
              @click="handleCreateOrganizationRoot"
              >添加根机构</a-button
            >
          </template>
          <BasicTree
            defaultExpandAll
            :clickRowToExpand="false"
            :treeData="treeData"
            :beforeRightClick="getRightMenuList"
            @select="handleSelect"
            ref="treeRef"
          />
        </Card>
      </Col>
      <Col :span="18">
        <Card title="成员" class="w-4/4 xl:w-5/5">
          <BasicTable
            @register="registerTable"
            class="w-4/4 xl:w-5/5"
            :searchInfo="searchInfo"
            ref="tableRef"
          >
            <template #toolbar>
              <a-button
                type="primary"
                @click="handleAddOrganizationUsersDrawer"
                v-auth="'Organization.AddUsers'"
                v-if="canAddOrganizationUsers"
                >添加成员</a-button
              >
            </template>
            <template #action="{ record }">
              <TableAction
                :actions="[
                  {
                    icon: 'ant-design:delete-outlined',
                    color: 'error',
                    tooltip: '移除此账号',
                    auth: 'Organization.RemoveUser',
                    popConfirm: {
                      title: '是否确认移除该用户',
                      confirm: handleRemoveUser.bind(null, record),
                    },
                  },
                ]"
              />
            </template>
          </BasicTable>
        </Card>
      </Col>
    </Row>
    <OrganizationDrawer
      @register="registerOrganizationDrawer"
      @success="handleCreateOrganization"
    />
    <OrganizationUserDrawer
      @register="registerOrganizationUserDrawer"
      @success="handleAddOrganizationUsers"
    />
    <OrganizationRoleDrawer
      @register="registerOrganizationRoleDrawer"
      @success="handleAllocationOrganizationRoles"
    />
    <OrganizationPositionDrawer
      @register="registerOrganizationPositionDrawer"
      @success="handleAllocationOrganizationPositions"
    />
    <OrganizationDetailDrawer @register="registerOrganizationDetailDrawer" />
  </PageWrapper>
</template>
<script lang="ts">
  import { PageWrapper } from '/@/components/Page';
  import { Card, Row, Col } from 'ant-design-vue';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { BasicTable, useTable, TableAction, TableActionType } from '/@/components/Table';
  import { BasicTree, TreeActionType, TreeItem, ContextMenuItem } from '/@/components/Tree';
  import { defineComponent, ref, unref, onMounted, reactive, nextTick, computed } from 'vue';
  import { Status } from '/@/utils/status';
  import {
    getOrganizationTree,
    getOrganizationUserPageList,
    getOrganizationById,
    updateOrganization,
    createOrganization,
    deleteOrganization,
    addOrganizationUsers,
    removeOrganizationUsers,
    setAllocationOrganizationRoles,
    setAllocationOrganizationPositions,
  } from '/@/api/organization';
  import { treeMap } from '/@/utils/helper/treeHelper';
  import { GetOrgizationTreeModel } from '/@/api/organization/model/organizationModel';
  import {
    getOrganizationRolesOptions,
    getOrganizationPositionsOptions,
    userColumns,
  } from './organization.data';
  import { useDrawer } from '/@/components/Drawer';
  import OrganizationDrawer from './OrganizationDrawer.vue';
  import OrganizationUserDrawer from './OrganizationUserDrawer.vue';
  import OrganizationDetailDrawer from './OrganizationDetailDrawer.vue';
  import OrganizationRoleDrawer from './OrganizationRoleDrawer.vue';
  import OrganizationPositionDrawer from './OrganizationPositionDrawer.vue';
  import { usePermission } from '/@/hooks/web/usePermission';
  import { getOrganizationTreeList } from './organization.data';
  export default defineComponent({
    name: 'OrganizationManagement',
    components: {
      Card,
      Row,
      Col,
      PageWrapper,
      BasicTree,
      BasicTable,
      TableAction,
      OrganizationDrawer,
      OrganizationUserDrawer,
      OrganizationDetailDrawer,
      OrganizationRoleDrawer,
      OrganizationPositionDrawer,
    },
    setup() {
      const treeRef = ref<Nullable<TreeActionType>>(null);
      const tableRef = ref<Nullable<TableActionType>>(null);
      const treeData = ref<TreeItem[]>([]);
      const canAddOrganizationUsers = ref<boolean>(false);
      const selectedOrganizationId = ref<Nullable<number>>();
      const searchInfo = reactive<Recordable>({});
      const loadingRef = ref(false);
      const emptyText = computed(() => {
        if (unref(selectedOrganizationId)) {
          return '暂无数据';
        }
        return '选择一个组织机构来查看成员';
      });
      const { hasPermission } = usePermission();
      const [registerOrganizationDrawer, { openDrawer: openOrganizationDrawer }] = useDrawer();
      const [registerOrganizationUserDrawer, { openDrawer: openOrganizationUserDrawer }] =
        useDrawer();
      const [registerOrganizationDetailDrawer, { openDrawer: openOrganizationDetailDrawer }] =
        useDrawer();
      const [registerOrganizationRoleDrawer, { openDrawer: openOrganizationRoleDrawer }] =
        useDrawer();
      const [registerOrganizationPositionDrawer, { openDrawer: openOrganizationPositionDrawer }] =
        useDrawer();
      const { createConfirm, notification } = useMessage();
      function getTree() {
        const tree = unref(treeRef);
        if (!tree) {
          throw new Error('tree is null!');
        }
        return tree;
      }

      function getTable() {
        const table = unref(tableRef);
        if (!table) {
          throw new Error('table is null!');
        }
        return table;
      }

      const tableConfig: any = {
        rowKey: 'id',
        columns: userColumns,
        api: getOrganizationUserPageList,
        immediate: false,
        locale: {
          emptyText: unref(emptyText),
        },
        showTableSetting: true,
      };

      if (hasPermission(['Organization.RemoveUser'])) {
        tableConfig.actionColumn = {
          width: 100,
          title: '操作',
          dataIndex: 'action',
          slots: { customRender: 'action' },
        };
      }
      const [registerTable, { reload, setTableData }] = useTable(tableConfig);

      onMounted(async () => {
        await loadOrganizationTreeData();
        getTree().expandAll(true);
      });

      function handleSelect(keys: number[]) {
        nextTick(async () => {
          if (keys.length > 0) {
            const orgId = keys[0];
            if (orgId != unref(selectedOrganizationId)) {
              selectedOrganizationId.value = orgId;
              searchInfo.id = orgId;
              getTable().setProps({
                locale: {
                  emptyText: unref(emptyText),
                },
              });
              await setCanAddOrganizationUsers(orgId);
              reload();
            }
          }
        });
      }

      function setCanAddOrganizationUsers(orgId: number) {
        nextTick(async () => {
          const orgInfo = await getOrganizationById(orgId);
          if (orgInfo.status == Status.Valid && orgInfo.isBelong) {
            canAddOrganizationUsers.value = true;
          } else {
            canAddOrganizationUsers.value = false;
          }
        });
      }

      function handleAddOrganizationUsersDrawer() {
        if (unref(canAddOrganizationUsers)) {
          openOrganizationUserDrawer(true, {
            id: selectedOrganizationId,
          });
        }
      }

      function handleCreateOrganizationRoot() {
        openOrganizationDrawer(true, {
          isUpdate: false,
          id: undefined,
        });
      }

      async function loadOrganizationTreeData() {
       // const organizationTreeList = await getOrganizationTree();
        treeData.value = await getOrganizationTreeList(true);
      }
      function handleCreateOrganization(data: any) {
        nextTick(async () => {
          try {
            loadingRef.value = true;
            const { isUpdate, values } = data;
            if (isUpdate) {
              await updateOrganization(values);
              loadingRef.value = false;
              notification.success({
                message: '更新组织机构成功',
              });
            } else {
              await createOrganization(values);
              loadingRef.value = false;
              notification.success({
                message: '创建组织机构成功',
              });
            }
            await loadOrganizationTreeData();
            if (unref(selectedOrganizationId) && unref(selectedOrganizationId) == values.id) {
              await setCanAddOrganizationUsers(values.id);
            }
          } catch (err) {
            loadingRef.value = false;
          }
        });
      }

      function handleAddOrganizationUsers(data: any) {
        nextTick(async () => {
          if (data.addUsers.length > 0) {
            await addOrganizationUsers(data);
            reload();
            notification.success({
              message: '增加组织机构用户成功',
            });
          } else {
            notification.warning({
              message: '您没有选择任何要添加的用户',
            });
          }
        });
      }
      function handleAllocationOrganizationRoles(data: any) {
        nextTick(async () => {
          loadingRef.value = true;
          try {
            await setAllocationOrganizationRoles(data.id, data.roleIds);
            loadingRef.value = false;
            notification.success({
              message: `分配组织机构${data.organizationName}的角色成功`,
            });
          } catch (err) {
            loadingRef.value = false;
          }
        });
      }

      function handleAllocationOrganizationPositions(data: any) {
        nextTick(async () => {
          loadingRef.value = true;
          try {
            await setAllocationOrganizationPositions(data.id, data.positionIds);
            loadingRef.value = false;
            notification.success({
              message: `分配组织机构${data.organizationName}的职位成功`,
            });
          } catch (err) {
            loadingRef.value = false;
          }
        });
      }

      function handleRemoveUser(record: Recordable) {
        nextTick(async () => {
          try {
            loadingRef.value = true;
            await removeOrganizationUsers(unref(selectedOrganizationId), [record.id]);
            loadingRef.value = false;
            reload();
            notification.success({
              message: `移除用户${record.userName}成功`,
            });
          } catch (err) {
            loadingRef.value = false;
          }
        });
      }

      async function getRightMenuList(node: any): Promise<ContextMenuItem[]> {
        const organizationInfo = await getOrganizationById(node.eventKey);
        let rigthMenList: ContextMenuItem[] = [];
        if (hasPermission('Organization.Update') && organizationInfo.isBelong) {
          rigthMenList.push({
            label: '编辑',
            handler: () => {
              openOrganizationDrawer(true, {
                isUpdate: true,
                id: node.eventKey,
              });
            },
            icon: 'clarity:note-edit-line',
          });
        }
        if (hasPermission('Organization.Create') && organizationInfo.isBelong) {
          rigthMenList.push({
            label: '添加子机构',
            handler: () => {
              openOrganizationDrawer(true, {
                isUpdate: false,
                id: node.eventKey,
              });
            },
            icon: 'bi:plus',
          });
        }
        if (
          hasPermission('Organization.AllocationRole') &&
          organizationInfo.status === Status.Valid &&
          organizationInfo.isBelong
        ) {
          rigthMenList.push({
            label: '分配角色',
            handler: () => {
              nextTick(async () => {
                const roleOptions = await getOrganizationRolesOptions();
                const roleIds = organizationInfo.organizationRoles.map((item) => item.roleId);
                openOrganizationRoleDrawer(true, {
                  roleOptions: roleOptions,
                  organizationId: organizationInfo.id,
                  organizationName: organizationInfo.name,
                  roleIds: roleIds,
                });
              });
            },
            icon: 'carbon:user-role',
          });
        }
        if (
          hasPermission('Organization.AllocationPosition') &&
          organizationInfo.status === Status.Valid &&
          organizationInfo.isBelong
        ) {
          rigthMenList.push({
            label: '分配职位',
            handler: () => {
              nextTick(async () => {
                const positionOptions = await getOrganizationPositionsOptions();
                const positionIds = organizationInfo.organizationPositions.map(
                  (item) => item.positionId,
                );
                openOrganizationPositionDrawer(true, {
                  positionOptions: positionOptions,
                  organizationId: organizationInfo.id,
                  organizationName: organizationInfo.name,
                  positionIds: positionIds,
                });
              });
            },
            icon: 'fluent:position-backward-20-regular',
          });
        }

        if (hasPermission('Organization.Delete') && organizationInfo.isBelong) {
          rigthMenList.push({
            label: '删除',
            handler: () => {
              createConfirm({
                title: '删除',
                content: '您是否确认删除该机构',
                onOk: async () => {
                  try {
                    loadingRef.value = true;
                    await deleteOrganization(node.eventKey);
                    loadingRef.value = false;
                    notification.success({
                      message: '删除机构成功',
                    });
                    await loadOrganizationTreeData();
                    if (node.eventKey === unref(selectedOrganizationId)) {
                      selectedOrganizationId.value = null;
                      canAddOrganizationUsers.value = false;
                      setTableData([]);
                    }
                  } catch (err) {
                    loadingRef.value = false;
                  }
                },
                iconType: 'warning',
              });
            },
            icon: 'ant-design:delete-outlined',
          });
        }
        if (hasPermission('Organization.LookDetail') && organizationInfo.isBelong) {
          rigthMenList.push({
            label: '查看',
            handler: () => {
              openOrganizationDetailDrawer(true, node.eventKey);
            },
            icon: 'clarity:info-standard-line',
          });
        }
        return Promise.resolve(rigthMenList);
      }
      return {
        treeData,
        treeRef,
        tableRef,
        canAddOrganizationUsers,
        searchInfo,
        loadingRef,
        getRightMenuList,
        handleSelect,
        registerTable,
        registerOrganizationDrawer,
        registerOrganizationUserDrawer,
        registerOrganizationDetailDrawer,
        registerOrganizationRoleDrawer,
        registerOrganizationPositionDrawer,
        handleCreateOrganization,
        handleCreateOrganizationRoot,
        handleAddOrganizationUsersDrawer,
        handleAddOrganizationUsers,
        handleAllocationOrganizationRoles,
        handleAllocationOrganizationPositions,
        handleRemoveUser,
      };
    },
  });
</script>
