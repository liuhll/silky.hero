<template>
  <PageWrapper>
    <BasicTable @register="registerTable" :searchInfo="searchInfo">
      <template #toolbar>
        <a-button type="primary" @click="handleCreate">新增岗位</a-button>
      </template>
      <template #action="{ record }">
        <TableAction
          :actions="[
            {
              icon: 'clarity:info-standard-line',
              tooltip: '查看岗位详情',
              onClick: handleView.bind(null, record),
            },
            {
              icon: 'clarity:note-edit-line',
              tooltip: '编辑岗位资料',
              onClick: handleEdit.bind(null, record),
            },
            {
              icon: 'ant-design:delete-outlined',
              color: 'error',
              tooltip: '删除此岗位',
              popConfirm: {
                title: '是否确认删除',
                confirm: handleDelete.bind(null, record),
              },
            },
          ]"
        />
      </template>
    </BasicTable>
    <PositionDrawer @register="registerDrawer" @success="handleSuccess" />
  </PageWrapper>
</template>

<script lang="ts">
  import { defineComponent, ref, nextTick } from 'vue';
  import { BasicTable, useTable, TableAction } from '/@/components/Table';
  import { getPositionPageList, createPosition, updatePosition, deletePosition } from '/@/api/position';
  import { columns, searchFormSchema } from './position.data';
  import { PageWrapper } from '/@/components/Page';
  import PositionDrawer from './PositionDrawer.vue';
  import { useDrawer } from '/@/components/Drawer';
  import { useMessage } from '/@/hooks/web/useMessage';
  export default defineComponent({
    name: 'Position',
    components: { PageWrapper, BasicTable, TableAction, PositionDrawer },
    setup() {
      const searchInfo = ref({});
      const { notification } = useMessage();
      const [registerTable, { reload }] = useTable({
        title: '岗位列表',
        rowKey: 'id',
        columns,
        api: getPositionPageList,
        formConfig: {
          labelWidth: 120,
          schemas: searchFormSchema,
          autoSubmitOnEnter: true,
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
      const [registerDrawer, { openDrawer }] = useDrawer();
      function handleCreate() {
        openDrawer(true, {
          isUpdate: false,
        });
      }
      function handleSuccess(data) {
        nextTick(async () => {
          const isUpdate = !!data?.isUpdate;
          if (isUpdate) {
            await updatePosition(data.values);
            // updateTableDataRecord(data.values.id, data.values);
            notification.success({
              message: `更新岗位${data.values.name}成功.`,
            });
          } else {
            await createPosition(data.values);
            notification.success({
              message: `新增岗位${data.values.name}成功.`,
            });
          }
          reload();
        });
      }
      function handleView(record: Recordable) {}
      function handleEdit(record: Recordable) {
        openDrawer(true, {
          isUpdate: true,
          record,
        });
      }
      function handleDelete(record: Recordable) {
        nextTick(async () => {
          await deletePosition(record.id);
          notification.success({
            message: `删除岗位${record.name}成功.`,
          });
          reload();
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
      };
    },
  });
</script>
