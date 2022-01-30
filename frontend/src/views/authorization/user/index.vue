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
      <template #form-positionIdSelect>
        <Select
          v-model:value="selectedPositionIds"
          placeholder="请选择"
          mode="multiple"
          allow-clear
          :options="positionOptions"
        />
      </template>
      <template #userSubsidiaries="{ record }">
        <Tag v-for="(userSubsidiary, index) in record.userSubsidiaries" :key="index" color="green">
          {{ displayUserSubsidiary(userSubsidiary) }}
        </Tag>
      </template>
      <template #toolbar>
        <a-button type="primary" @click="handleCreate">新增账号</a-button>
      </template>
    </BasicTable>
  </PageWrapper>
</template>

<script lang="ts">
  import { defineComponent, ref, onMounted, unref } from 'vue';
  import { TreeSelect, Tag, Select } from 'ant-design-vue';
  import { getUserPageList } from '/@/api/user';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { PageWrapper } from '/@/components/Page';
  import { columns, searchFormSchema } from './user.data';
  import { getOrganizationTree } from '/@/api/organization';
  import { GetOrgizationTreeModel } from '/@/api/organization/model/organizationModel';
  import { treeMap } from '/@/utils/helper/treeHelper';
  import { TreeItem } from '/@/components/Tree';
  import { getPositionList } from '/@/api/position';
  import { omit } from 'lodash-es';
  import { Status } from '/@/utils/status';

  type OptionsItem = { label: string; value: string; disabled?: boolean };

  export default defineComponent({
    name: 'User',
    components: { PageWrapper, BasicTable, TableAction, TreeSelect, Tag, Select },
    setup() {
      const searchInfo = ref({});
      const treeData = ref<TreeItem[]>([]);
      const selectedOrganizationIds = ref([]);
      const selectedPositionIds = ref([]);
      const positionOptions = ref<OptionsItem[]>([]);
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
        const positionList = await getPositionList({});
        positionOptions.value = positionList.reduce((prev, next: Recordable) => {
          if (next) {
            prev.push({
              ...omit(next, ['name', 'id']),
              label: next['name'],
              value: next['id'],
              disabled: next['status'] === Status.Invalid,
            });
          }
          return prev;
        }, [] as OptionsItem[]);
      });
      function displayUserSubsidiary(userSubsidiary) {
        return `${userSubsidiary.organizationName}(${userSubsidiary.positionName})`;
      }

      const [registerTable, { reload, updateTableDataRecord }] = useTable({
        title: '用户列表',
        rowKey: 'id',
        columns,
        api: getUserPageList,
        formConfig: {
          labelWidth: 120,
          schemas: searchFormSchema,
          autoSubmitOnEnter: true,
          resetFunc: function() {
            selectedOrganizationIds.value = [];
            selectedPositionIds.value = [];
          },
        },
        handleSearchInfoFn: (formData) => {
          const organizationIds = unref(selectedOrganizationIds);
          if (organizationIds.length > 0) {
            formData.OrganizationIds = organizationIds.join(',');
          }
          const positionIds = unref(selectedPositionIds);
          if (positionIds.length > 0) {
            formData.PositionIds = positionIds.join(',');
          }
          return formData;
        },
        useSearchForm: true,
        showTableSetting: true,
        bordered: true,
      });
      return {
        registerTable,
        displayUserSubsidiary,
        positionOptions,
        searchInfo,
        treeData,
        selectedOrganizationIds,
        selectedPositionIds,
      };
    },
  });
</script>
