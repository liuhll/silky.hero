<template>
  <PageWrapper>
    <BasicTable @register="registerTable" class="w-4/4 xl:w-5/5" :searchInfo="searchInfo">
      <template #form-organizationTree>
        <TreeSelect
          v-model:value="selectedOrganizationIds"
          show-search
          style="width: 100%"
          :tree-data="treeData"
          multiple
          allow-clear
          placeholder="请选择"
          tree-default-expand-all
        />
      </template>
      <template #toolbar>
        <a-button type="primary" @click="handleCreate">新增账号</a-button>
      </template>
    </BasicTable>
  </PageWrapper>
</template>

<script lang="ts">
  import { defineComponent, ref, onMounted, unref } from 'vue';
  import { TreeSelect } from 'ant-design-vue';
  import { getUserPageList } from '/@/api/user';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { PageWrapper } from '/@/components/Page';
  import { columns, searchFormSchema } from './user.data';
  import { getOrganizationTree } from '/@/api/organization';
  import { GetOrgizationTreeModel } from '/@/api/organization/model/organizationModel';
  import { treeMap } from '/@/utils/helper/treeHelper';
  import { TreeItem } from '/@/components/Tree';

  export default defineComponent({
    name: 'User',
    components: { PageWrapper, BasicTable, TableAction, TreeSelect },
    setup() {
      const searchInfo = ref({});
      const treeData = ref<TreeItem[]>([]);
      const selectedOrganizationIds = ref([]);
      onMounted(async () => {
        const organizationTreeList = await getOrganizationTree();
        treeData.value = treeMap(organizationTreeList, {
          conversion: (node: GetOrgizationTreeModel) => {
            return {
              title: node.name,
              key: node.id,
              value: node.id,
            };
          },
        });
      });

      const [registerTable, { reload, updateTableDataRecord }] = useTable({
        title: '用户列表',
        rowKey: 'id',
        columns,
        api: getUserPageList,
        formConfig: {
          labelWidth: 120,
          schemas: searchFormSchema,
          autoSubmitOnEnter: true,
        },
        handleSearchInfoFn: (formData) => {
          const organizationIds = unref(selectedOrganizationIds);
          if (organizationIds.length > 0) {
            formData.organizationIds = organizationIds.join(',');
          }
          return formData;
        },
        useSearchForm: true,
        showTableSetting: true,
        bordered: true,
      });
      return {
        registerTable,
        searchInfo,
        treeData,
        selectedOrganizationIds,
      };
    },
  });
</script>
