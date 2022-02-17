<template>
  <PageWrapper v-loading="loadingRef" loading-tip="加载中...">
    <BasicTable @register="registerTable" :searchInfo="searchInfo">
      <template #toolbar>
        <a-button type="primary" v-auth="'Tenant.Create'" @click="handleCreate">新增租户</a-button>
      </template>
      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              icon: 'clarity:info-standard-line',
              tooltip: '查看租户详情',
              onClick: handleView.bind(null, record),
              auth: 'Tenant.LookDetail',
            },
            {
              icon: 'clarity:note-edit-line',
              tooltip: '编辑租户资料',
              onClick: handleEdit.bind(null, record),
              auth: 'Tenant.Update',
            },
            {
              icon: 'ant-design:delete-outlined',
              color: 'error',
              tooltip: '删除此租户',
              popConfirm: {
                title: '是否确认删除',
                confirm: handleDelete.bind(null, record),
              },
              auth: 'Tenant.Delete',
            },
          ]"
        />
      </template>
    </BasicTable>
    <TenantDrawer @register="registerDrawer" @success="handleSuccess" />
    <TenantDetailDrawer @register="registerTenantDetailDrawer" ref="tenantDetailDrawerRef" />
  </PageWrapper>
</template>

<script lang="ts">
  import { defineComponent, ref, unref, computed, nextTick } from 'vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { getTenantPageList, createTenant, updateTenant, deleteTenant } from '/@/api/tenant';
  import { columns, searchFormSchema } from './tenant.data';
  import { PageWrapper } from '/@/components/Page';
  import TenantDrawer from './TenantDrawer.vue';
  import TenantDetailDrawer from './TenantDetailDrawer.vue';
  import { useDrawer } from '/@/components/Drawer';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { usePermission } from '/@/hooks/web/usePermission';
  export default defineComponent({
    name: 'Tenant',
    components: {
      PageWrapper,
      BasicTable,
      TableAction,
      TenantDrawer,
      TenantDetailDrawer,
    },
    setup() {
      const searchInfo = ref({});
      const { notification } = useMessage();
      const { hasPermission } = usePermission();
      const showSearchForm = computed(() => hasPermission('Tenant.Search'));
      const loadingRef = ref(false);
      const tableConfig: any = {
        title: '租户列表',
        rowKey: 'id',
        columns,
        api: getTenantPageList,
        formConfig: {
          labelWidth: 120,
          schemas: searchFormSchema,
          autoSubmitOnEnter: true,
        },
        useSearchForm: unref(showSearchForm),
        showTableSetting: true,
        bordered: true,
      };
      if (hasPermission(['Tenant.LookDetail', 'Tenant.Update', 'Tenant.Delete'])) {
        tableConfig.actionColumn = {
          width: 120,
          title: '操作',
          dataIndex: 'action',
          slots: { customRender: 'action' },
        };
      }
      const [registerTable, { reload }] = useTable(tableConfig);

      const [registerDrawer, { openDrawer }] = useDrawer();
      const [registerTenantDetailDrawer, { openDrawer: openTenantDetailDrawer }] = useDrawer();
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
              await updateTenant(data.values);
              loadingRef.value = false;
              // updateTableDataRecord(data.values.id, data.values);
              notification.success({
                message: `更新租户${data.values.name}成功.`,
              });
            } else {
              await createTenant(data.values);
              loadingRef.value = false;
              notification.success({
                message: `新增租户${data.values.name}成功.`,
              });
            }
            reload();
          } catch (err) {
            loadingRef.value = false;
          }
        });
      }

      function handleView(record: Recordable) {
        nextTick(() => {
          openTenantDetailDrawer(true, record.id);
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
            await deleteTenant(record.id);
            loadingRef.value = false;
            notification.success({
              message: `删除租户${record.name}成功.`,
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
        registerTenantDetailDrawer,
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
