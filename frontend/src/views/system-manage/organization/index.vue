<template>
  <PageWrapper>
    <Row :gutter="[24, 16]">
      <Col :span="6">
        <Card title="组织机构树">
          <template #extra>
            <a-button type="primary" @click="handleCreateOrganizationRoot">添加根机构</a-button>
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
                @click="handleAddOrganizationUsers"
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
                    tooltip: '删除此账号',
                    popConfirm: {
                      title: '是否确认删除',
                      confirm: handleDelete.bind(null, record),
                    },
                  },
                ]"
              />
            </template>
          </BasicTable>
        </Card>
      </Col>
    </Row>
    <OrganizationModal @register="registerOrganizationModal" @success="handleSuccess" />
    <OrganizationUserModal @register="registerOrganizationUserModal" />
  </PageWrapper>
</template>
<script lang="ts">
import { PageWrapper } from '/@/components/Page';
import { Card, Row, Col } from 'ant-design-vue';
import { useMessage } from '/@/hooks/web/useMessage';
import { BasicTable, useTable, TableAction, TableActionType } from '/@/components/Table';
import { BasicTree, TreeActionType, TreeItem, ContextMenuItem } from '/@/components/Tree';
import { defineComponent, ref, unref, onMounted, reactive } from 'vue';
import { Status } from '/@/utils/status';
import {
  getOrganizationTree,
  getOrganizationUserPageList,
  getOrganizationById,
  updateOrganization,
  createOrganization,
  deleteOrganization,
} from '/@/api/organization';
import { treeMap } from '/@/utils/helper/treeHelper';
import { GetOrgizationTreeModel } from '/@/api/organization/model/organizationModel';
import { userColumns } from './organization.data';
import { useModal } from '/@/components/Modal';
import OrganizationModal from './OrganizationModal.vue';
import OrganizationUserModal from './OrganizationUserModal.vue';

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
    OrganizationModal,
    OrganizationUserModal,
  },
  setup() {
    const treeRef = ref<Nullable<TreeActionType>>(null);
    const tableRef = ref<Nullable<TableActionType>>(null);
    const treeData = ref<TreeItem[]>([]);
    const canAddOrganizationUsers = ref<boolean>(false);
    const selectedOrganizationId = ref<number | undefined>();
    const searchInfo = reactive<Recordable>({});
    const [registerOrganizationModal, { openModal: openOrganizationModal }] = useModal();
    const [registerOrganizationUserModal, { openModal: openOrganizationUserModal }] = useModal();
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

    const [registerTable, { reload, updateTableDataRecord }] = useTable({
      rowKey: 'id',
      columns: userColumns,
      api: getOrganizationUserPageList,
      immediate: false,
      locale: {
        emptyText: '选择一个组织机构来查看成员',
      },
    });
    onMounted(async () => {
      await loadOrganizationTreeData();
      getTree().expandAll(true);
    });

    async function handleSelect(keys: number[]) {
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
    }

    async function setCanAddOrganizationUsers(orgId: number) {
      const orgInfo = await getOrganizationById(orgId);
      if (orgInfo.status == Status.Valid) {
        canAddOrganizationUsers.value = true;
      } else {
        canAddOrganizationUsers.value = false;
      }
    }

    function handleAddOrganizationUsers() {
      if (canAddOrganizationUsers) {
        openOrganizationUserModal(true, {
          id: selectedOrganizationId,
        });
      }
    }

    function handleCreateOrganizationRoot() {
      openOrganizationModal(true, {
        isUpdate: false,
        id: undefined,
      });
    }

    async function loadOrganizationTreeData() {
      const organizationTreeList = await getOrganizationTree();
      treeData.value = treeMap(organizationTreeList, {
        conversion: (node: GetOrgizationTreeModel) => {
          const orgIcon =
            node.status == Status.Valid ? 'ant-design:folder-outlined' : 'ant-design:folder-filled';
          return {
            title: node.name,
            key: node.id,
            icon: orgIcon,
          };
        },
      });
    }
    async function handleSuccess(data: any) {
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
    }
    function getRightMenuList(node: any): ContextMenuItem[] {
      return [
        {
          label: '编辑',
          handler: () => {
            openOrganizationModal(true, {
              isUpdate: true,
              id: node.eventKey,
            });
          },
          icon: 'clarity:note-edit-line',
        },
        {
          label: '添加子机构',
          handler: () => {
            openOrganizationModal(true, {
              isUpdate: false,
              id: node.eventKey,
            });
          },
          icon: 'bi:plus',
        },
        {
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
                  await loadOrganizationTreeData();
                });
              },
            });
          },
          icon: 'ant-design:delete-outlined',
        },
      ];
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
      registerOrganizationModal,
      registerOrganizationUserModal,
      handleSuccess,
      handleCreateOrganizationRoot,
      handleAddOrganizationUsers,
    };
  },
});
</script>
