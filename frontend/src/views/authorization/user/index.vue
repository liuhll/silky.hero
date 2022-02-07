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
        <a-button type="primary" @click="handleCreate">新增账号</a-button>
      </template>
      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              icon: 'clarity:info-standard-line',
              tooltip: '查看用户详情',
              onClick: handleView.bind(null, record),
            },
            {
              icon: 'clarity:note-edit-line',
              tooltip: '编辑用户资料',
              onClick: handleEdit.bind(null, record),
            },
            {
              icon: 'carbon:user-role',
              tooltip: '授权角色',
              onClick: handleAuthorizeRole.bind(null, record),
            },
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
    <UserDrawer @register="registerDrawer" @success="handleSuccess" />
    <UserRoleDrawer @register="registerUserRoleDrawer" @success="handleSuccessAuthorizeUserRole" />
  </PageWrapper>
</template>

<script lang="ts">
  import { defineComponent, ref, onMounted, unref, nextTick } from 'vue';
  import { TreeSelect, Tag, Select } from 'ant-design-vue';
  import {
    getUserPageList,
    createUser,
    updateUser,
    deleteUser,
    getUserRoles,
    updateUserRoles,
  } from '/@/api/user';
  import { getRoleList } from '/@/api/role';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { PageWrapper } from '/@/components/Page';
  import { columns, searchFormSchema } from './user.data';
  import { getOrganizationTreeList } from '/@/views/authorization/organization/organization.data';
  import { TreeItem } from '/@/components/Tree';
  import UserDrawer from './UserDrawer.vue';
  import UserRoleDrawer from './UserRoleDrawer.vue';
  import { useDrawer } from '/@/components/Drawer';
  import { getPositionOptions } from '/@/views/authorization/position/position.data';
  import { OptionsItem } from '/@/utils/model';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { Status } from '/@/utils/status';
  export default defineComponent({
    name: 'User',
    components: {
      PageWrapper,
      BasicTable,
      TableAction,
      TreeSelect,
      Tag,
      Select,
      UserDrawer,
      UserRoleDrawer,
    },
    setup() {
      const searchInfo = ref({});
      const treeData = ref<TreeItem[]>([]);
      const selectedOrganizationIds = ref([]);
      const selectedPositionIds = ref([]);
      const positionOptions = ref<OptionsItem[]>([]);
      const [registerDrawer, { openDrawer }] = useDrawer();
      const [registerUserRoleDrawer, { openDrawer: openUserRoleDrawer }] = useDrawer();
      const { notification } = useMessage();
      onMounted(async () => {
        treeData.value = await getOrganizationTreeList();
        positionOptions.value = await getPositionOptions({});
      });

      function displayUserSubsidiary(userSubsidiary) {
        return `${userSubsidiary.organizationName}(${userSubsidiary.positionName})`;
      }

      function handleCreate() {
        openDrawer(true, {
          isUpdate: false,
        });
      }

      function handleEdit(record: Recordable) {
        openDrawer(true, {
          isUpdate: true,
          record,
        });
      }

      function handleView(record: Recordable) {}

      function handleDelete(record: Recordable) {
        nextTick(async () => {
          await deleteUser(record.id);
          notification.success({
            message: `删除用户${record.userName}成功.`,
          });
          reload();
        });
      }

      function handleAuthorizeRole(record: Recordable) {
        nextTick(async () => {
          const roleList = await getRoleList({});
          const roleOptions = roleList.map((item) => {
            return {
              label: item.realName,
              value: item.name,
              disabled: item.status === Status.Invalid,
            };
          });
          const userRoleInfo = await getUserRoles(record.id);
          openUserRoleDrawer(true, {
            roleOptions: roleOptions,
            userId: record.id,
            roleNames: userRoleInfo.roleNames,
            userName: record.userName,
          });
        });
      }

      function handleSuccess(data) {
        nextTick(async () => {
          const isUpdate = !!data?.isUpdate;
          if (isUpdate) {
            await updateUser(data.values);
            //updateTableDataRecord(data.values.id, data.values);
            notification.success({
              message: `更新用户${data.values.userName}成功.`,
            });
          } else {
            await createUser(data.values);
            notification.success({
              message: `新增用户${data.values.userName}成功.`,
            });
          }
          reload();
        });
      }

      function handleSuccessAuthorizeUserRole(data) {
        nextTick(async () => {
          await updateUserRoles(data.id, data.roleNames);
          notification.success({
            message: `更新用户${data.userName}角色成功.`,
          });
        });
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
        actionColumn: {
          width: 120,
          title: '操作',
          dataIndex: 'action',
          slots: { customRender: 'action' },
        },
      });

      return {
        registerTable,
        displayUserSubsidiary,
        handleCreate,
        handleEdit,
        handleView,
        handleDelete,
        handleAuthorizeRole,
        registerDrawer,
        handleSuccess,
        registerUserRoleDrawer,
        handleSuccessAuthorizeUserRole,
        positionOptions,
        searchInfo,
        treeData,
        selectedOrganizationIds,
        selectedPositionIds,
      };
    },
  });
</script>
