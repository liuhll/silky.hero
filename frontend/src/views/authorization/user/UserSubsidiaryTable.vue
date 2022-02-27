<template>
  <div>
    <BasicTable @register="registerTable" @edit-change="handleEditChange">
      <template #action="{ record, column }">
        <TableAction :actions="createActions(record, column)" />
      </template>
    </BasicTable>
    <a-button block class="mt-5" type="dashed" @click="handleAdd" v-if="isEditable">
      增行
    </a-button>
  </div>
</template>
<script lang="ts">
  import { defineComponent, ref, unref, computed } from 'vue';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { getPositionOptions } from '/@/views/authorization/position/position.data';
  import { getOrganizationTreeList } from '/@/views/authorization/organization/organization.data';
  import { OptionsItem } from '/@/utils/model';
  import { TreeItem } from '/@/components/Tree';
  const positionOptions = ref<OptionsItem[]>([]);
  const organizaionTreeList = ref<TreeItem[]>([]);
  import {
    BasicTable,
    useTable,
    TableAction,
    BasicColumn,
    ActionItem,
    EditRecordRow,
  } from '/@/components/Table';
  import { checkOrganizationDataRange } from '/@/api/organization';
  import { checkPositionDataRange } from '/@/api/position';

  export default defineComponent({
    components: { BasicTable, TableAction },
    name: 'UserSubsidiaryTable',
    props: {
      editable: {
        type: Boolean,
        default: true,
      },
    },
    setup(props) {
      const isEditable = computed(() => props.editable);
      const columns: BasicColumn[] = [
        {
          title: '所属部门',
          dataIndex: 'organizationId',
          editRow: true,
          editRule: async (text, record) => {
            if (!text) {
              notification.error({
                message: '请选择用户所属部门',
              });
              return Promise.reject('请选择用户所属部门');
            }
            if (!(await checkOrganizationDataRange(Number(text)))) {
              notification.error({
                message: '您没有新增该部门用户的权限',
              });
              record.positionId = null;
              await setPositionOptions(text);
              return Promise.reject('您没有新增该部门用户的权限');
            }
            return Promise.resolve('');
          },
          editComponent: 'TreeSelect',
          editComponentProps: {
            treeData: unref(organizaionTreeList),
            treeDefaultExpandAll: true,
            placeholder: '请选择所属部门',
          },
        },
        {
          title: '岗位',
          dataIndex: 'positionId',
          editRow: true,
          editComponent: 'Select',
          editRule: async (text, record) => {
            if (!text) {
              notification.error({
                message: '请选择用户所属岗位',
              });
              return Promise.reject('请选择用户所属岗位');
            }
            if (!(await checkPositionDataRange(Number(record.organizationId), Number(text)))) {
              notification.error({
                message: `没有分配该岗位的权限`,
              });
              return Promise.reject(`没有分配该岗位的权限`);
            }
            return Promise.resolve('');
          },
          editComponentProps: {
            options: unref(positionOptions),
            placeholder: '请选择所属岗位',
          },
        },
      ];
      const { notification } = useMessage();
      const tableConfig: any = {
        columns: columns,
        showIndexColumn: false,
        pagination: false,
      };
      if (unref(isEditable)) {
        tableConfig.actionColumn = {
          width: 140,
          title: '操作',
          dataIndex: 'action',
          slots: { customRender: 'action' },
        };
      }
      const [
        registerTable,
        { getDataSource, setTableData, getRawDataSource, getColumns, setProps },
      ] = useTable(tableConfig);

      async function handleEdit(record: EditRecordRow) {
        if (
          unref(positionOptions)
            .map((item) => item.value)
            .indexOf(record.positionId) < 0
        ) {
          record.positionId = null;
        }
        await setPositionOptions(record.organizationId);
        record.onEdit?.(true);
      }

      async function setOrganizaionTreeList() {
        organizaionTreeList.value = await getOrganizationTreeList(true);
        const tableColumns = getColumns();
        const organizationColumn = tableColumns.find((col) => col.dataIndex === 'organizationId');
        organizationColumn.editComponentProps.treeData = unref(organizaionTreeList);
        setProps({
          columns: tableColumns,
        });
      }

      async function setPositionOptions(id: Nullable<Number>) {
        positionOptions.value = await getPositionOptions(id, true);
        const tableColumns = getColumns();
        const positionColumn = tableColumns.find((col) => col.dataIndex === 'positionId');
        positionColumn.editComponentProps.options = unref(positionOptions);
        setProps({
          columns: tableColumns,
        });
      }

      function clearPositionOptions() {
        positionOptions.value = [];
        const tableColumns = getColumns();
        const positionColumn = tableColumns.find((col) => col.dataIndex === 'positionId');
        positionColumn.editComponentProps.options = unref(positionOptions);
        setProps({
          columns: tableColumns,
        });
      }

      function handleCancel(record: EditRecordRow) {
        if (record.isNew) {
          const data = getDataSource();
          const index = data.findIndex((item) => item.key === record.key);
          data.splice(index, 1);
        } else {
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
        }
        record.onEdit?.(false);
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

      async function handleEditChange(data: Recordable) {
        if (data.column.dataIndex == 'positionId') {
          data.record.positionId = data.value;
        }
        if (data.column.dataIndex == 'organizationId') {
          if (hasOrganization(data.value)) {
            throw new Error('用户所属部门不允许重复');
          }
          if (data.record.organizationId !== data.value) {
            data.record.positionId = null;
            await setPositionOptions(data.value);
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
          organizationId: null,
          positionId: null,
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
        setOrganizaionTreeList,
        setPositionOptions,
        handleEditChange,
        isEditable,
      };
    },
  });
</script>
