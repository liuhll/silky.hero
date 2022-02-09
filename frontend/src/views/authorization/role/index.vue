<template>
  <PageWrapper>
    <BasicTable @register="registerTable" :searchInfo="searchInfo">
      <template #realName="{ text, record }">
        {{ text }}
        <Tag color="green" v-if="record.isDefault">默认</Tag>
        <Tag color="blue" v-if="record.isPublic">公开</Tag>
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
  </PageWrapper>
</template>

<script lang="ts">
  import { defineComponent, ref, unref, computed, nextTick } from 'vue';
  import { Tag } from 'ant-design-vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { getMenuTreeList2 } from '/@/views/authorization/menu/menu.data';
  import { getRoleMenuIds } from '/@/api/role';
  import { treeToList } from '/@/utils/helper/treeHelper';
  import { arrayEquals } from '/@/utils';

  import {
    getRolePageList,
    createRole,
    updateRole,
    deleteRole,
    updateRoleMenuIds,
    getRoleDataRange,
    updateRoleDataRange,
  } from '/@/api/role';
  import { columns, searchFormSchema } from './role.data';
  import { PageWrapper } from '/@/components/Page';
  import RoleDrawer from './RoleDrawer.vue';
  import RoleMenuDrawer from './RoleMenuDrawer.vue';
  import RoleDataDrawer from './RoleDataDrawer.vue';
  import { TreeItem } from '/@/components/Tree';
  import { useDrawer } from '/@/components/Drawer';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { getOrganizationTreeList } from '/@/views/authorization/organization/organization.data';
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
    },
    setup() {
      const searchInfo = ref({});
      const { notification } = useMessage();
      const { hasPermission } = usePermission();
      const showSearchForm = computed(() => hasPermission('Identity.Role.Search'));
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
          'Identity.Role.Create',
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
      function handleCreate() {
        openDrawer(true, {
          isUpdate: false,
        });
      }
      function handleSuccess(data) {
        nextTick(async () => {
          const isUpdate = !!data?.isUpdate;
          if (isUpdate) {
            await updateRole(data.values);
            // updateTableDataRecord(data.values.id, data.values);
            notification.success({
              message: `更新角色${data.values.name}成功.`,
            });
          } else {
            await createRole(data.values);
            notification.success({
              message: `新增角色${data.values.name}成功.`,
            });
          }
          reload();
        });
      }

      function handleSuccessAuthorizeRoleMenu(data) {
        nextTick(async () => {
          await updateRoleMenuIds(data);
          notification.success({
            message: `更新角色菜单权限成功.`,
          });
        });
      }

      function handleSuccessAuthorizeRoleData(data) {
        nextTick(async () => {
          await updateRoleDataRange(data);
          notification.success({
            message: `更新角色数据权限成功.`,
          });
        });
      }

      function handleView(record: Recordable) {}

      function isCheckedAll(menuTree: TreeItem[], checkedMenuIds: number[]) {
        const allMenuIds = treeToList(menuTree)
          .map((item) => item.key)
          .sort((item1, item2) => {
            return item1 - item2;
          });

        return arrayEquals(
          allMenuIds,
          checkedMenuIds.sort((item1, item2) => {
            return item1 - item2;
          }),
        );
      }

      function handleAuthorizeMenu(record: Recordable) {
        nextTick(async () => {
          openRoleMenuDrawer(true, {
            record,
          });
          const menuTree = await getMenuTreeList2({});
          roleMenuDrawerRef.value?.setMenusTreeData(menuTree);
          const roleMenu = await getRoleMenuIds(record.id);
          roleMenuDrawerRef.value?.getTree().setCheckedKeys(roleMenu.menuIds);
          const checkedAll = isCheckedAll(menuTree, roleMenu.menuIds);
          const indeterminate = !checkedAll && roleMenu.menuIds.length > 0;
          roleMenuDrawerRef.value?.setCheckAllStateStatus(indeterminate, checkedAll);
        });
      }

      function handleAuthorizeDataRange(record: Recordable) {
        nextTick(async () => {
          const roleDataRange = await getRoleDataRange(record.id);
          const orgTreeData = await getOrganizationTreeList();
          openRoleDataDrawer(true, {
            roleDataRange: roleDataRange,
            orgTreeData: orgTreeData,
          });
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
          await deleteRole(record.id);
          notification.success({
            message: `删除角色${record.name}成功.`,
          });
          reload();
        });
      }
      return {
        registerTable,
        handleCreate,
        registerDrawer,
        registerRoleMenuDrawer,
        registerRoleDataDrawer,
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
      };
    },
  });
</script>
