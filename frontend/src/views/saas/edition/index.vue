<template>
  <PageWrapper v-loading="loadingRef" loading-tip="加载中...">
    <BasicTable @register="registerTable" :searchInfo="searchInfo">
      <template #toolbar>
        <a-button type="primary" v-auth="'Edition.Create'" @click="handleCreate">新增版本</a-button>
      </template>
      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              icon: 'clarity:info-standard-line',
              tooltip: '查看版本详情',
              onClick: handleView.bind(null, record),
              auth: 'Edition.LookDetail',
            },
            {
              icon: 'clarity:note-edit-line',
              tooltip: '编辑版本资料',
              onClick: handleEdit.bind(null, record),
              auth: 'Edition.Update',
            },
            {
              icon: 'ant-design:delete-outlined',
              color: 'error',
              tooltip: '删除此版本',
              popConfirm: {
                title: '是否确认删除',
                confirm: handleDelete.bind(null, record),
              },
              auth: 'Edition.Delete',
            },
          ]"
        />
      </template>
    </BasicTable>
    <EditionDrawer @register="registerDrawer" @success="handleSuccess" />
  </PageWrapper>
</template>

<script lang="ts">
  import { defineComponent, ref, unref, computed, nextTick } from 'vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { getEditionPageList, createEdition, updateEdition, deleteEdition } from '/@/api/edition';
  import { columns, searchFormSchema } from './edition.data';
  import { PageWrapper } from '/@/components/Page';
  import EditionDrawer from './EditionDrawer.vue';
  import { useDrawer } from '/@/components/Drawer';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { usePermission } from '/@/hooks/web/usePermission';
  export default defineComponent({
    name: 'Edition',
    components: {
      PageWrapper,
      BasicTable,
      TableAction,
      EditionDrawer,
    },
    setup() {
      const searchInfo = ref({});
      const { notification } = useMessage();
      const { hasPermission } = usePermission();
      const showSearchForm = computed(() => hasPermission('Edition.Search'));
      const loadingRef = ref(false);
      const tableConfig: any = {
        title: '版本列表',
        rowKey: 'id',
        columns,
        api: getEditionPageList,
        formConfig: {
          labelWidth: 120,
          schemas: searchFormSchema,
          autoSubmitOnEnter: true,
        },
        useSearchForm: unref(showSearchForm),
        showTableSetting: true,
        bordered: true,
      };
      if (hasPermission(['Edition.LookDetail', 'Edition.Update', 'Edition.Delete'])) {
        tableConfig.actionColumn = {
          width: 120,
          title: '操作',
          dataIndex: 'action',
          slots: { customRender: 'action' },
        };
      }
      const [registerTable, { reload }] = useTable(tableConfig);

      const [registerDrawer, { openDrawer }] = useDrawer();
      const [registerEditionDetailDrawer, { openDrawer: openEditionDetailDrawer }] = useDrawer();
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
              await updateEdition(data.values);
              loadingRef.value = false;
              // updateTableDataRecord(data.values.id, data.values);
              notification.success({
                message: `更新版本${data.values.name}成功.`,
              });
            } else {
              await createEdition(data.values);
              loadingRef.value = false;
              notification.success({
                message: `新增版本${data.values.name}成功.`,
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
          openEditionDetailDrawer(true, record.id);
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
            await deleteEdition(record.id);
            loadingRef.value = false;
            notification.success({
              message: `删除版本${record.name}成功.`,
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
