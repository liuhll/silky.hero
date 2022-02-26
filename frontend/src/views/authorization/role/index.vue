<template>
  <PageWrapper v-loading="loadingRef" loading-tip="加载中...">
    <BasicTable @register="registerTable" :searchInfo="searchInfo">
      <template #realName="{ text, record }">
        {{ text }}
        <Tag color="green" v-if="record.isDefault" style="margin-left: 3px">默认</Tag>
        <Tag color="blue" v-if="record.isPublic" style="margin-left: 3px">公共</Tag>
        <Tag color="cyan" v-if="record.isStatic" style="margin-left: 3px">静态</Tag>
      </template>
      <template #toolbar>
        <a-button type="primary" v-auth="'Identity.Role.Create'" @click="handleCreate"
          >新增角色</a-button
        >
      </template>
      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              icon: 'clarity:info-standard-line',
              tooltip: '查看角色详情',
              onClick: handleView.bind(null, record),
              auth: 'Identity.Role.LookDetail',
            },
            {
              icon: 'clarity:note-edit-line',
              tooltip: '编辑角色资料',
              onClick: handleEdit.bind(null, record),
              auth: 'Identity.Role.Update',
            },
            {
              icon: 'clarity:menu-line',
              tooltip: '授权菜单',
              onClick: handleAuthorizeMenu.bind(null, record),
              auth: 'Identity.Role.SetMenus',
            },
            {
              icon: 'mdi:database-outline',
              tooltip: '授权数据',
              onClick: handleAuthorizeDataRange.bind(null, record),
              auth: 'Identity.Role.SetDataRange',
            },
            {
              icon: 'ant-design:delete-outlined',
              color: 'error',
              tooltip: '删除此角色',
              popConfirm: {
                title: '是否确认删除',
                confirm: handleDelete.bind(null, record),
              },
              auth: 'Identity.Role.Delete',
            },
          ]"
        />
      </template>
    </BasicTable>
    <RoleDrawer @register="registerDrawer" @success="handleSuccess" />
    <RoleMenuDrawer
      @register="registerRoleMenuDrawer"
      @success="handleSuccessAuthorizeRoleMenu"
      ref="roleMenuDrawerRef"
    />
    <RoleDataDrawer
      @register="registerRoleDataDrawer"
      @success="handleSuccessAuthorizeRoleData"
      ref="roleDataDrawerRef"
    />
    <RoleDetailDrawer @register="registerRoleDetailDrawer" />
  </PageWrapper>
</template>

<script lang="ts">
  import { defineComponent, ref, unref, computed, nextTick } from 'vue';
  import { Tag } from 'ant-design-vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import {
    getRolePageList,
    createRole,
    updateRole,
    deleteRole,
    updateRoleMenuIds,
    updateRoleDataRange,
  } from '/@/api/role';
  import { columns, searchFormSchema } from './role.data';
  import { PageWrapper } from '/@/components/Page';
  import RoleDrawer from './RoleDrawer.vue';
  import RoleMenuDrawer from './RoleMenuDrawer.vue';
  import RoleDataDrawer from './RoleDataDrawer.vue';
  import RoleDetailDrawer from './RoleDetailDrawer.vue';
  import { TreeItem } from '/@/components/Tree';
  import { useDrawer } from '/@/components/Drawer';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { usePermission } from '/@/hooks/web/usePermission';
  export default defineComponent({
    name: 'Role',
    components: {
      PageWrapper,
      BasicTable,
      TableAction,
      Tag,
      RoleDrawer,
      RoleMenuDrawer,
      RoleDataDrawer,
      RoleDetailDrawer,
    },
    setup() {
      const searchInfo = ref({});
      const { notification } = useMessage();
      const { hasPermission } = usePermission();
      const showSearchForm = computed(() => hasPermission('Identity.Role.Search'));
      const loadingRef = ref(false);
      const tableConfig: any = {
        title: '角色列表',
        rowKey: 'id',
        columns,
        api: getRolePageList,
        formConfig: {
          labelWidth: 120,
          schemas: searchFormSchema,
          autoSubmitOnEnter: true,
        },
        useSearchForm: unref(showSearchForm),
        showTableSetting: true,
        bordered: true,
      };
      if (
        hasPermission([
          'Identity.Role.LookDetail',
          'Identity.Role.Update',
          'Identity.Role.Delete',
          'Identity.Role.SetMenus',
          'Identity.Role.SetDataRange',
        ])
      ) {
        tableConfig.actionColumn = {
          width: 120,
          title: '操作',
          dataIndex: 'action',
          slots: { customRender: 'action' },
        };
      }
      const [registerTable, { reload }] = useTable(tableConfig);
      const roleMenuDrawerRef = ref<{
        getTree: () => any;
        setMenusTreeData: (treeData: TreeItem[]) => void;
        setCheckAllStateStatus: (indeterminate: boolean, checkAll: boolean) => void;
      }>('roleMenuDrawerRef');

      const [registerDrawer, { openDrawer }] = useDrawer();
      const [registerRoleMenuDrawer, { openDrawer: openRoleMenuDrawer }] = useDrawer();
      const [registerRoleDataDrawer, { openDrawer: openRoleDataDrawer }] = useDrawer();
      const [registerRoleDetailDrawer, { openDrawer: openRoleDetailDrawer }] = useDrawer();
      function handleCreate() {
        openDrawer(true, {
          isUpdate: false,
        });
      }
      function handleSuccess(data) {
        nextTick(async () => {
          try {
            loadingRef.value = true;
            const isUpdate = !!data?.isUpdate;
            if (isUpdate) {
              await updateRole(data.values);
              loadingRef.value = false;
              // updateTableDataRecord(data.values.id, data.values);
              notification.success({
                message: `更新角色${data.values.name}成功.`,
              });
            } else {
              await createRole(data.values);
              loadingRef.value = false;
              notification.success({
                message: `新增角色${data.values.name}成功.`,
              });
            }
            reload();
          } catch (err) {
            loadingRef.value = false;
          }
        });
      }

      function handleSuccessAuthorizeRoleMenu(data) {
        nextTick(async () => {
          try {
            loadingRef.value = true;
            await updateRoleMenuIds(data);
            loadingRef.value = false;
            notification.success({
              message: `更新角色菜单权限成功.`,
            });
          } catch (err) {
            loadingRef.value = false;
          }
        });
      }

      function handleSuccessAuthorizeRoleData(data) {
        nextTick(async () => {
          try {
            loadingRef.value = true;
            await updateRoleDataRange(data);
            loadingRef.value = false;
            notification.success({
              message: `更新角色数据权限成功.`,
            });
          } catch (err) {
            loadingRef.value = false;
          }
        });
      }

      function handleView(record: Recordable) {
        nextTick(() => {
          openRoleDetailDrawer(true, record.id);
        });
      }

      function handleAuthorizeMenu(record: Recordable) {
        nextTick(async () => {
          openRoleMenuDrawer(true, {
            record,
          });
        });
      }

      function handleAuthorizeDataRange(record: Recordable) {
        nextTick(async () => {
          openRoleDataDrawer(true, record.id);
        });
      }

      function handleEdit(record: Recordable) {
        openDrawer(true, {
          isUpdate: true,
          record,
        });
      }
      function handleDelete(record: Recordable) {
        nextTick(async () => {
          try {
            loadingRef.value = true;
            await deleteRole(record.id);
            loadingRef.value = false;
            notification.success({
              message: `删除角色${record.name}成功.`,
            });
            reload();
          } catch (err) {
            loadingRef.value = false;
          }
        });
      }
      return {
        registerTable,
        handleCreate,
        registerDrawer,
        registerRoleMenuDrawer,
        registerRoleDataDrawer,
        registerRoleDetailDrawer,
        handleSuccess,
        handleView,
        handleEdit,
        handleDelete,
        handleAuthorizeMenu,
        handleAuthorizeDataRange,
        handleSuccessAuthorizeRoleMenu,
        handleSuccessAuthorizeRoleData,
        searchInfo,
        roleMenuDrawerRef,
        loadingRef,
      };
    },
  });
</script>
