<template>
  <PageWrapper v-loading="loadingRef" loading-tip="加载中...">
    <BasicTable @register="registerTable" :searchInfo="searchInfo">
      <template #toolbar>
        <a-button type="primary" v-auth="'Position.Create'" @click="handleCreate"
          >新增岗位</a-button
        >
      </template>
      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              icon: 'clarity:info-standard-line',
              tooltip: '查看岗位详情',
              onClick: handleView.bind(null, record),
              auth: 'Position.LookDetail',
            },
            {
              icon: 'clarity:note-edit-line',
              tooltip: '编辑岗位资料',
              onClick: handleEdit.bind(null, record),
              auth: 'Position.Update',
            },
            {
              icon: 'ant-design:delete-outlined',
              color: 'error',
              tooltip: '删除此岗位',
              popConfirm: {
                title: '是否确认删除',
                confirm: handleDelete.bind(null, record),
              },
              auth: 'Position.Delete',
            },
          ]"
        />
      </template>
    </BasicTable>
    <PositionDrawer @register="registerDrawer" @success="handleSuccess" />
    <PositionDetailDrawer @register="registerPositionDetailDrawer" />
  </PageWrapper>
</template>

<script lang="ts">
  import { defineComponent, ref, unref, computed, nextTick } from 'vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import {
    getPositionPageList,
    createPosition,
    updatePosition,
    deletePosition,
  } from '/@/api/position';
  import { columns, searchFormSchema } from './position.data';
  import { PageWrapper } from '/@/components/Page';
  import PositionDrawer from './PositionDrawer.vue';
  import PositionDetailDrawer from './PositionDetailDrawer.vue';
  import { useDrawer } from '/@/components/Drawer';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { usePermission } from '/@/hooks/web/usePermission';
  export default defineComponent({
    name: 'Position',
    components: { PageWrapper, BasicTable, TableAction, PositionDrawer, PositionDetailDrawer },
    setup() {
      const searchInfo = ref({});
      const loadingRef = ref(false);
      const { notification } = useMessage();
      const { hasPermission } = usePermission();
      const showSearchForm = computed(() => hasPermission('Position.Search'));
      const tableConfig: any = {
        title: '岗位列表',
        rowKey: 'id',
        columns,
        api: getPositionPageList,
        formConfig: {
          labelWidth: 120,
          schemas: searchFormSchema,
          autoSubmitOnEnter: true,
        },
        useSearchForm: unref(showSearchForm),
        showTableSetting: true,
        bordered: true,
      };
      if (hasPermission(['Position.LookDetail', 'Position.Update', 'Position.Delete'])) {
        tableConfig.actionColumn = {
          width: 120,
          title: '操作',
          dataIndex: 'action',
          slots: { customRender: 'action' },
        };
      }
      const [registerTable, { reload }] = useTable(tableConfig);
      const [registerDrawer, { openDrawer }] = useDrawer();
      const [registerPositionDetailDrawer, {openDrawer: openPositionDetailDrawer }] = useDrawer();
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
              await updatePosition(data.values);
              loadingRef.value = false;
              // updateTableDataRecord(data.values.id, data.values);
              notification.success({
                message: `更新岗位${data.values.name}成功.`,
              });
            } else {
              await createPosition(data.values);
              loadingRef.value = false;
              notification.success({
                message: `新增岗位${data.values.name}成功.`,
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
          openPositionDetailDrawer(true, record.id);
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
            await deletePosition(record.id);
            loadingRef.value = false;
            notification.success({
              message: `删除岗位${record.name}成功.`,
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
        registerPositionDetailDrawer,
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
