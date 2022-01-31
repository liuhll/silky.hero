<template>
  <PageWrapper>
    <BasicTable @register="registerTable" :searchInfo="searchInfo">
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
        <a-button type="primary" @click="handleCreateUserDrawer">新增账号</a-button>
      </template>
    </BasicTable>
    <UserDrawer @register="registerDrawer" @success="handleCreateUser" />
  </PageWrapper>
</template>

<script lang="ts">
  import { defineComponent, ref, onMounted, unref } from 'vue';
  import { TreeSelect, Tag, Select } from 'ant-design-vue';
  import { getUserPageList, createUser } from '/@/api/user';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { PageWrapper } from '/@/components/Page';
  import { columns, searchFormSchema } from './user.data';
  import { getOrganizationTreeList } from '/@/views/authorization/organization/organization.data';
  import { TreeItem } from '/@/components/Tree';
  import UserDrawer from './UserDrawer.vue';
  import { useDrawer } from '/@/components/Drawer';
  import { getPositionOptions } from '/@/views/authorization/position/position.data';
  import { OptionsItem } from '/@/utils/model';
  import { useMessage } from '/@/hooks/web/useMessage';
  export default defineComponent({
    name: 'User',
    components: { PageWrapper, BasicTable, TableAction, TreeSelect, Tag, Select, UserDrawer },
    setup() {
      const searchInfo = ref({});
      const treeData = ref<TreeItem[]>([]);
      const selectedOrganizationIds = ref([]);
      const selectedPositionIds = ref([]);
      const positionOptions = ref<OptionsItem[]>([]);
      const [registerDrawer, { openDrawer }] = useDrawer();
      const { notification } = useMessage();
      onMounted(async () => {
        treeData.value = await getOrganizationTreeList();
        positionOptions.value = await getPositionOptions({});
      });

      function displayUserSubsidiary(userSubsidiary) {
        return `${userSubsidiary.organizationName}(${userSubsidiary.positionName})`;
      }

      function handleCreateUserDrawer() {
        openDrawer(true, {
          isUpdate: false,
        });
      }
      async function handleCreateUser(data) {
        await createUser(data);
        notification.success({
          message: `新增用户${data.userName}成功.`,
        });
        reload();
      }

      const [registerTable, { reload }] = useTable({
        title: '用户列表',
        rowKey: 'id',
        columns,
        api: getUserPageList,
        formConfig: {
          labelWidth: 120,
          schemas: searchFormSchema,
          autoSubmitOnEnter: true,
          resetFunc: () => {
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
        handleCreateUserDrawer,
        registerDrawer,
        handleCreateUser,
        positionOptions,
        searchInfo,
        treeData,
        selectedOrganizationIds,
        selectedPositionIds,
      };
    },
  });
</script>
