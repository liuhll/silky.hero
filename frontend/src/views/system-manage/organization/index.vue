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
          <BasicTable @register="registerTable" class="w-4/4 xl:w-5/5" :searchInfo="searchInfo" ref="tableRef">
            <template #toolbar>
              <a-button type="primary" @click="handleCreate" v-if="selectedOrganization"
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
  </PageWrapper>
</template>
<script lang="ts">
import { PageWrapper } from '/@/components/Page';
import { Card, Row, Col } from 'ant-design-vue';
import { BasicTable, useTable, TableAction, TableActionType } from '/@/components/Table';
import { BasicTree, TreeActionType, TreeItem, ContextMenuItem } from '/@/components/Tree';
import { defineComponent, ref, unref, onMounted, reactive } from 'vue';
import { getOrganizationTree, getOrganizationUserPageList } from '/@/api/organization';
import { treeMap } from '/@/utils/helper/treeHelper';
import { GetOrgizationTreeModel } from '/@/api/organization/model/organizationModel';
import { userColumns } from './organization.data';

export default defineComponent({
  components: { Card, Row, Col, PageWrapper, BasicTree, BasicTable, TableAction },
  setup() {
    const treeRef = ref<Nullable<TreeActionType>>(null);
    const tableRef = ref<Nullable<TableActionType>>(null);
    const treeData = ref<TreeItem[]>([]);
    const selectedOrganization = ref<boolean>(false);
    const selectedOrganizationId = ref<number|undefined>();
    const searchInfo = reactive<Recordable>({});
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
        emptyText: '选择一个组织机构来查看成员'
      }
    });
    onMounted(async () => {
      await loadOrganizationTreeData();
      getTree().expandAll(true);
    });

    function handleSelect(keys: number[]) {
      if (keys.length > 0) {
        const orgId = keys[0];
        if (orgId != selectedOrganization.value) {
          selectedOrganization.value = orgId;
          searchInfo.id = orgId;
          getTable().setProps({
            locale: {
              emptyText: '暂无数据'
            }
          });
          reload();
        }
      }
    }

    async function loadOrganizationTreeData() {
      const organizationTreeList = await getOrganizationTree();
      treeData.value = treeMap(organizationTreeList, {
        conversion: (node: GetOrgizationTreeModel) => {
          return {
            title: node.name,
            key: node.id,
            icon: 'ant-design:folder-outlined',
          };
        },
      });
    }
    function getRightMenuList(node: any): ContextMenuItem[] {
      return [
        {
          label: '编辑',
          handler: () => {
            console.log('点击了新增', node);
          },
          icon: 'clarity:note-edit-line',
        },
         {
          label: '添加子机构',
          handler: () => {
            console.log('添加子机构', node);
          },
          icon: 'bi:plus',
        },
        {
          label: '删除',
          handler: () => {
            console.log('点击了删除', node);
          },
          icon: 'ant-design:delete-outlined',
        },
      ];
    }
    return {
      treeData,
      treeRef,
      tableRef,
      selectedOrganization,
      searchInfo,
      getRightMenuList,
      handleSelect,
      registerTable,
    };
  },
});
</script>
