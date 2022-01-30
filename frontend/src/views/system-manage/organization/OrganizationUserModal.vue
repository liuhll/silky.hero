<template>
  <BasicModal
    v-bind="$attrs"
    @register="registerModal"
    :title="getTitle"
    @ok="handleSubmit"
    :width="800"
  >
    <BasicTable @register="registerTable" :searchInfo="searchInfo" ref="tableRef" :maxHeight="400">
      <template #position="{ record }">
        <Tag v-if="isCheckedUser(record.id) && record.positionName" color="green">{{
          record.positionName
        }}</Tag>
        <Select
          v-if="!isCheckedUser(record.id)"
          v-model:value="record.positionId"
          size="small"
          style="width: 120px"
          placeholder="请选择职位信息"
          :options="positionOptions"
        />
      </template>
    </BasicTable>
  </BasicModal>
</template>

<script lang="ts">
  import { defineComponent, reactive, ref, unref, computed } from 'vue';
  import { Tag, Select } from 'ant-design-vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicTable, useTable, TableActionType } from '/@/components/Table';
  import { getOrganizationUserPage } from '/@/api/user';
  import { getOrganizationUserIds, getOrganizationById } from '/@/api/organization';
  import { GetOrgizationModel } from '/@/api/organization/model/organizationModel';
  import { organizationUserColumns } from './organization.data';
  import { getPositionList } from '/@/api/position';
  import { omit } from 'lodash-es';
  import { Status } from '/@/utils/status';
  import { useMessage } from '/@/hooks/web/useMessage';

  type OptionsItem = { label: string; value: string; disabled?: boolean };

  export default defineComponent({
    name: 'OrganizationUserModal',
    components: { BasicModal, BasicTable, Tag, Select },
    setup(_, { emit }) {
      const selectedOrganizationUserIds = ref<number[]>([]);
      const searchInfo = reactive<Recordable>({});
      const tableRef = ref<Nullable<TableActionType>>(null);
      const positionOptions = ref<OptionsItem[]>([]);
      const { notification } = useMessage();

      const organizationInfo = ref<GetOrgizationModel>({});

      const getTitle = computed(() => `添加【${unref(organizationInfo).name}】成员`);

      const [registerTable, { reload, setSelectedRowKeys }] = useTable({
        rowKey: 'id',
        columns: organizationUserColumns,
        immediate: false,
        clickToRowSelect: false,
        api: getOrganizationUserPage,
        rowSelection: {
          type: 'checkbox',
          getCheckboxProps(record: Recordable) {
            if (isCheckedUser(record.id)) {
              return { disabled: true };
            } else {
              return { disabled: false };
            }
          },
        },
      });

      function isCheckedUser(userId: number) {
        return unref(selectedOrganizationUserIds).indexOf(userId) >= 0;
      }

      function getTableAction() {
        const tableAction = unref(tableRef);
        if (!tableAction) {
          throw new Error('tableAction is null');
        }
        return tableAction;
      }
      const [registerModal, { closeModal, setModalProps }] = useModalInner(async (data: any) => {
        searchInfo.id = data.id;
        organizationInfo.value = await getOrganizationById(data.id);
        var organizationUserIds = await getOrganizationUserIds(data.id);
        selectedOrganizationUserIds.value = organizationUserIds;
        setSelectedRowKeys(organizationUserIds);
        const positionList = await getPositionList({});
        positionOptions.value = positionList.reduce((prev, next: Recordable) => {
          if (next) {
            prev.push({
              ...omit(next, ['name', 'id']),
              label: next['name'],
              value: next['id'],
              disabled: next['status'] === Status.Invalid,
            });
          }
          return prev;
        }, [] as OptionsItem[]);
        reload();
      });

      async function handleSubmit() {
        try {
          setModalProps({ confirmLoading: true });
          const selectedRows = getTableAction().getSelectRows();
          const addUsers = selectedRows.map((item) => {
            if (!item.positionId) {
              debugger;

              notification.warning({
                message: `请先为用户${item.userName}选择职位信息`,
              });
              throw Error(`请先为用户${item.userName}选择职位信息`);
            }
            return { userId: item.id, positionId: item.positionId };
          });

          closeModal();
          emit('success', { organizationUserId: searchInfo.id, addUsers });
        } finally {
          setModalProps({ confirmLoading: false });
        }
      }

      return {
        registerTable,
        registerModal,
        handleSubmit,
        isCheckedUser,
        searchInfo,
        tableRef,
        positionOptions,
        getTitle,
      };
    },
  });
</script>

<style></style>
