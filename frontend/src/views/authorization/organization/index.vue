<template>
  <PageWrapper>
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
  </PageWrapper>
</template>
<script lang="ts">
  import { PageWrapper } from '/@/components/Page';
  import { Card, Row, Col } from 'ant-design-vue';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { BasicTable, useTable, TableAction, TableActionType } from '/@/components/Table';
  import { BasicTree, TreeActionType, TreeItem, ContextMenuItem } from '/@/components/Tree';
  import { defineComponent, ref, unref, onMounted, reactive, nextTick } from 'vue';
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
  } from '/@/api/organization';
  import { treeMap } from '/@/utils/helper/treeHelper';
  import { GetOrgizationTreeModel } from '/@/api/organization/model/organizationModel';
  import { userColumns } from './organization.data';
  import { useDrawer } from '/@/components/Drawer';
  import OrganizationDrawer from './OrganizationDrawer.vue';
  import OrganizationUserDrawer from './OrganizationUserDrawer.vue';
  import { usePermission } from '/@/hooks/web/usePermission';
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
    },
    setup() {
      const treeRef = ref<Nullable<TreeActionType>>(null);
      const tableRef = ref<Nullable<TableActionType>>(null);
      const treeData = ref<TreeItem[]>([]);
      const canAddOrganizationUsers = ref<boolean>(false);
      const selectedOrganizationId = ref<number | undefined>();
      const searchInfo = reactive<Recordable>({});
      const { hasPermission } = usePermission();
      const [registerOrganizationDrawer, { openDrawer: openOrganizationDrawer }] = useDrawer();
      const [registerOrganizationUserDrawer, { openDrawer: openOrganizationUserDrawer }] =
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
          emptyText: '选择一个组织机构来查看成员',
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
      const [registerTable, { reload }] = useTable(tableConfig);

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
                  emptyText: '暂无数据',
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
          if (orgInfo.status == Status.Valid) {
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
        const organizationTreeList = await getOrganizationTree();
        treeData.value = treeMap(organizationTreeList, {
          conversion: (node: GetOrgizationTreeModel) => {
            const orgIcon =
              node.status == Status.Valid
                ? 'ant-design:folder-outlined'
                : 'ant-design:folder-filled';
            return {
              title: node.name,
              key: node.id,
              icon: orgIcon,
            };
          },
        });
      }
      function handleCreateOrganization(data: any) {
        nextTick(async () => {
          const { isUpdate, values } = data;
          if (isUpdate) {
            await updateOrganization(values);
            notification.success({
              message: '更新组织机构成功',
            });
          } else {
            await createOrganization(values);
            notification.success({
              message: '创建组织机构成功',
            });
          }
          await loadOrganizationTreeData();
          if (unref(selectedOrganizationId) && unref(selectedOrganizationId) == values.id) {
            await setCanAddOrganizationUsers(values.id);
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

      function handleRemoveUser(record: Recordable) {
        nextTick(async () => {
          await removeOrganizationUsers(unref(selectedOrganizationId), [record.id]);
          reload();
          notification.success({
            message: `移除用户${record.userName}成功`,
          });
        });
      }

      function getRightMenuList(node: any): ContextMenuItem[] {
        let rigthMenList: ContextMenuItem[] = [];
        if (hasPermission('Organization.Update')) {
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
        if (hasPermission('Organization.Create')) {
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
        if (hasPermission('Organization.Delete')) {
          rigthMenList.push({
            label: '删除',
            handler: () => {
              createConfirm({
                title: '删除',
                content: '您是否确认删除该机构',
                onOk: () => {
                  deleteOrganization(node.eventKey).then(async () => {
                    notification.success({
                      message: '删除机构成功',
                    });
                    selectedOrganizationId.value = undefined;
                    canAddOrganizationUsers.value = false;
                    await loadOrganizationTreeData();
                    reload();
                  });
                },
              });
            },
            icon: 'ant-design:delete-outlined',
          });
        }
        return rigthMenList;
      }
      return {
        treeData,
        treeRef,
        tableRef,
        canAddOrganizationUsers,
        searchInfo,
        getRightMenuList,
        handleSelect,
        registerTable,
        registerOrganizationDrawer,
        registerOrganizationUserDrawer,
        handleCreateOrganization,
        handleCreateOrganizationRoot,
        handleAddOrganizationUsersDrawer,
        handleAddOrganizationUsers,
        handleRemoveUser,
      };
    },
  });
</script>
