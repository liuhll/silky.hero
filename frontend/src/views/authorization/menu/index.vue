<template>
  <PageWrapper v-loading="loadingRef" loading-tip="加载中...">
    <BasicTable @register="registerTable" :searchInfo="searchInfo">
      <template #toolbar>
        <a-button type="primary" v-auth="'Permission.Menu.Create'" @click="handleCreate"
          >新增菜单</a-button
        >
      </template>
      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              icon: 'clarity:info-standard-line',
              tooltip: '查看菜单详情',
              onClick: handleView.bind(null, record),
              auth: 'Permission.Menu.LookDetail',
            },
            {
              icon: 'clarity:note-edit-line',
              tooltip: '编辑菜单资料',
              onClick: handleEdit.bind(null, record),
              auth: 'Permission.Menu.Update',
            },
            {
              icon: 'ant-design:delete-outlined',
              color: 'error',
              tooltip: '删除此菜单',
              popConfirm: {
                title: '是否确认删除',
                confirm: handleDelete.bind(null, record),
                auth: 'Permission.Menu.Delete',
              },
            },
          ]"
        />
      </template>
    </BasicTable>
    <MenuDrawer @register="registerDrawer" @success="handleSuccess" />
    <MenuDetailDrawer @register="registerMenuDetailDrawer" @success="handleSuccess" />
  </PageWrapper>
</template>

<script lang="ts">
  import { defineComponent, ref, unref, computed, nextTick } from 'vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { getMenuTree, createMenu, updateMenu, deleteMenu } from '/@/api/menu';
  import { columns, searchFormSchema } from './menu.data';
  import { PageWrapper } from '/@/components/Page';
  import MenuDrawer from './MenuDrawer.vue';
  import MenuDetailDrawer from './MenuDetailDrawer.vue';
  import { useDrawer } from '/@/components/Drawer';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { usePermission } from '/@/hooks/web/usePermission';
  export default defineComponent({
    name: 'Menu',
    components: { PageWrapper, BasicTable, TableAction, MenuDrawer, MenuDetailDrawer },
    setup() {
      const searchInfo = ref({});
      const { notification } = useMessage();
      const loadingRef = ref(false);
      const { hasPermission } = usePermission();
      const showSearchForm = computed(() => hasPermission('Permission.Menu.Search'));
      const tableConfig: any = {
        title: '菜单列表',
        rowKey: 'id',
        columns,
        api: getMenuTree,
        formConfig: {
          labelWidth: 120,
          schemas: searchFormSchema,
          autoSubmitOnEnter: true,
        },
        useSearchForm: unref(showSearchForm),
        isTreeTable: true,
        pagination: false,
        striped: false,
        showTableSetting: true,
        bordered: true,
        showIndexColumn: false,
        canResize: false,
      };
      if (
        hasPermission([
          'Permission.Menu.LookDetail',
          'Permission.Menu.Update',
          'Permission.Menu.Delete',
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
      const [registerDrawer, { openDrawer }] = useDrawer();
      const [registerMenuDetailDrawer, { openDrawer: openMenuDetailDrawer }] = useDrawer();
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
              await updateMenu(data.values);
              loadingRef.value = false;
              // updateTableDataRecord(data.values.id, data.values);
              notification.success({
                message: `更新菜单${data.values.name}成功.`,
              });
            } else {
              await createMenu(data.values);
              loadingRef.value = false;
              notification.success({
                message: `新增菜单${data.values.name}成功.`,
              });
            }
            reload();
          } catch (err) {
            loadingRef.value = false;
          }
        });
      }
      function handleView(record: Recordable) {
        openMenuDetailDrawer(true, record.id);
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
            await deleteMenu(record.id);
            loadingRef.value = false;
            notification.success({
              message: `删除菜单${record.name}成功.`,
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
        registerMenuDetailDrawer,
        handleSuccess,
        handleView,
        handleEdit,
        handleDelete,
        searchInfo,
        loadingRef,
      };
    },
  });
</script>
