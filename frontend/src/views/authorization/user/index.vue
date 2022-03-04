<template>
  <PageWrapper v-loading="loadingRef" loading-tip="加载中...">
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
      <template #form-roleIdSelect>
        <Select
          v-model:value="selectedRoleIds"
          placeholder="请选择"
          mode="multiple"
          allow-clear
          :options="roleOptions"
        />
      </template>
      <template #userSubsidiaries="{ text }">
        <Tag v-for="(userSubsidiary, index) in text" :key="index" color="blue" style="margin: 2px">
          {{ displayUserSubsidiary(userSubsidiary) }}
        </Tag>
      </template>
      <template #roles="{ text }">
        <Tag
          v-for="(role, index) in text"
          :key="index"
          :color="setRoleColor(role.status)"
          style="margin: 3px"
        >
          {{ role.realName }}
        </Tag>
      </template>
      <template #isLockout="{ text, record }">
        <Tooltip v-if="text" style="margin: 3px" color="red">
          <template #title>
            <span>{{ formatToDate(record.lockoutEnd, 'YYYY-MM-DD HH:mm:ss') }}</span>
          </template>
          <Tag color="red" style="margin: 3px"> 是 </Tag>
        </Tooltip>
        <Tag v-if="!text" color="blue" style="margin: 3px"> 否 </Tag>
      </template>
      <template #toolbar>
        <a-button type="primary" v-auth="'Identity.User.Create'" @click="handleCreate"
          >新增账号</a-button
        >
      </template>
      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              icon: 'clarity:info-standard-line',
              tooltip: '查看用户详情',
              onClick: handleView.bind(null, record),
              auth: 'Identity.User.LookDetail',
            },
            {
              icon: 'clarity:note-edit-line',
              tooltip: '编辑用户资料',
              onClick: handleEdit.bind(null, record),
              auth: 'Identity.User.Update',
            },
            {
              icon: 'carbon:user-role',
              tooltip: '授权角色',
              onClick: handleAuthorizeRole.bind(null, record),
              auth: 'Identity.User.SetRoles',
            },
            {
              icon: 'akar-icons:lock-on',
              tooltip: '锁定账号一段时间',
              onClick: handleLock.bind(null, record),
              ifShow: !record.isLockout,
              auth: 'Identity.User.Lock',
            },
            {
              icon: 'akar-icons:lock-off',
              tooltip: '解锁',
              color: 'error',
              popConfirm: {
                title: '是否解锁该用户?',
                confirm: handleUnLock.bind(null, record),
              },
              ifShow: record.isLockout,
              auth: 'Identity.User.UnLock',
            },
            {
              icon: 'ant-design:delete-outlined',
              color: 'error',
              tooltip: '删除此账号',
              popConfirm: {
                title: '是否确认删除',
                confirm: handleDelete.bind(null, record),
              },
              auth: 'Identity.User.Delete',
            },
          ]"
        />
      </template>
    </BasicTable>
    <UserDrawer @register="registerDrawer" @success="handleSuccess" />
    <UserRoleDrawer @register="registerUserRoleDrawer" @success="handleSuccessAuthorizeUserRole" />
    <UserLockModal @register="registerUserLockModal" @success="handleSuccessUserLock" />
    <UserDetailDrawer @register="registerUserDetailDrawer" />
  </PageWrapper>
</template>

<script lang="ts">
  import { defineComponent, ref, onMounted, unref, nextTick, computed } from 'vue';
  import { TreeSelect, Tag, Select, Tooltip } from 'ant-design-vue';
  import {
    getUserById,
    getUserPageList,
    createUser,
    updateUser,
    deleteUser,
    getUserRoles,
    updateUserRoles,
    lockUser,
    unLockUser,
  } from '/@/api/user';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { PageWrapper } from '/@/components/Page';
  import { columns, searchFormSchema } from './user.data';
  import { getOrganizationTreeList } from '/@/views/authorization/organization/organization.data';
  import { TreeItem } from '/@/components/Tree';
  import UserDrawer from './UserDrawer.vue';
  import UserRoleDrawer from './UserRoleDrawer.vue';
  import UserDetailDrawer from './UserDetailDrawer.vue';
  import UserLockModal from './UserLockModal.vue';
  import { useDrawer } from '/@/components/Drawer';
  import { useModal } from '/@/components/Modal';
  import { getPositionOptions } from '/@/views/authorization/position/position.data';
  import { getRoleOptions, getUserRoleOptions } from '/@/views/authorization/role/role.data';
  import { OptionsItem } from '/@/utils/model';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { Status } from '/@/utils/status';
  import { usePermission } from '/@/hooks/web/usePermission';
  import { formatToDate } from '/@/utils/dateUtil';
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
      UserDetailDrawer,
      UserLockModal,
      Tooltip,
    },
    setup() {
      const searchInfo = ref({});
      const treeData = ref<TreeItem[]>([]);
      const selectedOrganizationIds = ref([]);
      const selectedPositionIds = ref([]);
      const selectedRoleIds = ref([]);
      const positionOptions = ref<OptionsItem[]>([]);
      const roleOptions = ref<OptionsItem[]>([]);
      const [registerDrawer, { openDrawer }] = useDrawer();
      const [registerUserRoleDrawer, { openDrawer: openUserRoleDrawer }] = useDrawer();
      const [registerUserDetailDrawer, { openDrawer: openUserDetailDrawer }] = useDrawer();
      const [registerUserLockModal, { openModal: openUserLockModal }] = useModal();
      const { notification } = useMessage();
      const { hasPermission } = usePermission();
      const loadingRef = ref(false);
      const showSearchForm = computed(() => hasPermission('Identity.User.Search'));
      onMounted(async () => {
        treeData.value = await getOrganizationTreeList(true);
        positionOptions.value = await getPositionOptions(null, true);
        roleOptions.value = await getRoleOptions({});
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

      function handleView(record: Recordable) {
        nextTick(async () => {
          const userModel = await getUserById(record.id);
          openUserDetailDrawer(true, userModel);
        });
      }

      function handleDelete(record: Recordable) {
        nextTick(async () => {
          try {
            loadingRef.value = true;
            await deleteUser(record.id);
            loadingRef.value = false;
            notification.success({
              message: `删除用户${record.userName}成功.`,
            });
            reload();
          } catch (err) {
            loadingRef.value = false;
          }
        });
      }

      function handleLock(record: Recordable) {
        openUserLockModal(true, record);
      }

      function handleSuccessUserLock(data: any) {
        nextTick(async () => {
          try {
            loadingRef.value = true;
            await lockUser(data.id, data.lockoutSeconds * 60);
            loadingRef.value = false;
            notification.success({
              message: `锁定用户${data.realName}成功.`,
            });
            reload();
          } catch (err) {
            loadingRef.value = false;
          }
        });
      }

      function handleUnLock(record: Recordable) {
        nextTick(async () => {
          try {
            loadingRef.value = true;
            await unLockUser(record.id);
            loadingRef.value = false;
            notification.success({
              message: `解锁用户${record.realName}成功.`,
            });
            reload();
          } catch (err) {
            loadingRef.value = false;
          }
        });
      }

      function handleAuthorizeRole(record: Recordable) {
        nextTick(async () => {
          const roleOptions = await getUserRoleOptions(record.id);
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
          try {
            loadingRef.value = true;
            const isUpdate = !!data?.isUpdate;
            if (isUpdate) {
              await updateUser(data.values);
              loadingRef.value = false;
              //updateTableDataRecord(data.values.id, data.values);
              notification.success({
                message: `更新用户${data.values.userName}成功.`,
              });
            } else {
              await createUser(data.values);
              loadingRef.value = false;
              notification.success({
                message: `新增用户${data.values.userName}成功.`,
              });
            }
            reload();
          } catch (err) {
            loadingRef.value = false;
          }
        });
      }

      function setRoleColor(status: Status) {
        if (status === Status.Invalid) {
          return 'red';
        }
        return 'blue';
      }

      function handleSuccessAuthorizeUserRole(data) {
        nextTick(async () => {
          try {
            loadingRef.value = true;
            await updateUserRoles(data.id, data.roleNames);
            loadingRef.value = false;
            notification.success({
              message: `更新用户${data.userName}角色成功.`,
            });
            reload();
          } catch (err) {
            loadingRef.value = false;
          }
        });
      }

      const tableConfig: any = {
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
            selectedRoleIds.value = [];
          },
        },
        beforeFetch: (formData) => {
          const organizationIds = unref(selectedOrganizationIds);
          if (organizationIds.length > 0) {
            formData.OrganizationIds = organizationIds.join(',');
          }
          const positionIds = unref(selectedPositionIds);
          if (positionIds.length > 0) {
            formData.PositionIds = positionIds.join(',');
          }
          const roleIds = unref(selectedRoleIds);
          if (roleIds.length > 0) {
            formData.RoleIds = roleIds.join(',');
          }
          return formData;
        },
        useSearchForm: unref(showSearchForm),
        showTableSetting: true,
        bordered: true,
      };

      if (
        hasPermission([
          'Identity.User.LookDetail',
          'Identity.User.Update',
          'Identity.User.Lock',
          'Identity.User.UnLock',
          'Identity.User.Delete',
          'Identity.User.SetUsers',
        ])
      ) {
        tableConfig.actionColumn = {
          width: 130,
          title: '操作',
          dataIndex: 'action',
          slots: { customRender: 'action' },
        };
      }

      const [registerTable, { reload }] = useTable(tableConfig);

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
        handleSuccessUserLock,
        registerUserRoleDrawer,
        registerUserDetailDrawer,
        registerUserLockModal,
        handleSuccessAuthorizeUserRole,
        setRoleColor,
        formatToDate,
        handleUnLock,
        handleLock,
        positionOptions,
        roleOptions,
        searchInfo,
        treeData,
        selectedOrganizationIds,
        selectedPositionIds,
        selectedRoleIds,
        loadingRef,
      };
    },
  });
</script>
