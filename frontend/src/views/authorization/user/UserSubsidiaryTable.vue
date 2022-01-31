<template>
  <div>
    <BasicTable @register="registerTable" @edit-change="handleEditChange">
      <template #action="{ record, column }">
        <TableAction :actions="createActions(record, column)" />
      </template>
    </BasicTable>
    <a-button block class="mt-5" type="dashed" @click="handleAdd"> 增行 </a-button>
  </div>
</template>
<script lang="ts">
  import { defineComponent } from 'vue';
  import { useMessage } from '/@/hooks/web/useMessage';
  import {
    BasicTable,
    useTable,
    TableAction,
    BasicColumn,
    ActionItem,
    EditRecordRow,
  } from '/@/components/Table';

  const props = {
    positionOptions: [],
    organizationTreeList: [],
  };

  export default defineComponent({
    components: { BasicTable, TableAction },
    props,
    setup(props) {
      const columns: BasicColumn[] = [
        {
          title: '所属部门',
          dataIndex: 'organizationId',
          editRow: true,
          editComponent: 'TreeSelect',
          editComponentProps: {
            treeData: props.organizationTreeList,
            treeDefaultExpandAll: true,
            placeholder: '请选择所属部门',
          },
        },
        {
          title: '岗位',
          dataIndex: 'positionId',
          editRow: true,
          editComponent: 'Select',
          editComponentProps: {
            options: props.positionOptions,
            placeholder: '请选择所属岗位',
          },
        },
      ];
      const { notification } = useMessage();
      const [registerTable, { getDataSource, setTableData, getRawDataSource }] = useTable({
        columns: columns,
        showIndexColumn: false,
        actionColumn: {
          width: 140,
          title: '操作',
          dataIndex: 'action',
          slots: { customRender: 'action' },
        },
        pagination: false,
      });

      function handleEdit(record: EditRecordRow) {
        record.onEdit?.(true);
      }

      function handleCancel(record: EditRecordRow) {
        record.onEdit?.(false);
        if (record.isNew) {
          const data = getDataSource();
          const index = data.findIndex((item) => item.key === record.key);
          data.splice(index, 1);
        }
      }

      function handleSave(record: EditRecordRow) {
        if (!record.organizationId) {
          notification.error({
            message: '请选择用户所属部门',
          });
          throw new Error('请选择用户所属部门');
        }
        if (!record.positionId) {
          notification.error({
            message: '请选择用户所属岗位',
          });
          throw new Error('请选择用户所属岗位');
        }
        if (hasOrganization(record.organizationId)) {
          notification.error({
            message: '用户所属部门不允许重复',
          });
          throw new Error('用户所属部门不允许重复');
        }
        record.onEdit?.(false, true);
      }

      function hasOrganization(orgId: number) {
        const data = getDataSource();
        const exsitOrganizations = data.filter((item) => `${item.organizationId}` === `${orgId}`);
        return exsitOrganizations.length > 1;
      }

      function handleEditChange(data: Recordable) {
        if (data.column.dataIndex == 'positionId') {
          data.record.positionId = data.value;
        }
        if (data.column.dataIndex == 'organizationId') {
          if (hasOrganization(data.value)) {
            throw new Error('用户所属部门不允许重复');
          }
          data.record.organizationId = data.value;
        }
      }

      function handleDelete(record: EditRecordRow) {
        const data = getDataSource();
        const index = data.findIndex(
          (item) =>
            item.positionId === record.positionId && item.organizationId === record.organizationId,
        );
        data.splice(index, 1);
      }

      function handleAdd() {
        const data = getDataSource();
        const addRow: EditRecordRow = {
          organizationId: undefined,
          positionId: undefined,
          editable: true,
          isNew: true,
          key: `${Date.now()}`,
        };
        data.push(addRow);
      }

      function createActions(record: EditRecordRow, column: BasicColumn): ActionItem[] {
        if (!record.editable) {
          return [
            {
              label: '编辑',
              onClick: handleEdit.bind(null, record),
            },
            {
              label: '删除',
              popConfirm: {
                title: '是否确认删除',
                confirm: handleDelete.bind(null, record),
              },
            },
          ];
        }
        return [
          {
            label: '保存',
            onClick: handleSave.bind(null, record, column),
          },
          {
            label: '取消',
            popConfirm: {
              title: '是否取消编辑',
              confirm: handleCancel.bind(null, record, column),
            },
          },
        ];
      }

      return {
        registerTable,
        handleEdit,
        createActions,
        handleAdd,
        getDataSource,
        getRawDataSource,
        setTableData,
        handleEditChange,
      };
    },
  });
</script>
