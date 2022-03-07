<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    :title="getTitle"
    @ok="handleSubmit"
    showFooter
    width="900px"
    destroyOnClose
  >
    <BasicTable @register="registerTable" :searchInfo="searchInfo" ref="tableRef" :maxHeight="650">
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
      <template #isLeader="{ record }">
        <Tag v-if="isCheckedUser(record.id)" color="green">{{
          record.isLeader == true ? '是' : '否'
        }}</Tag>
        <Select
          v-if="!isCheckedUser(record.id)"
          v-model:value="record.isLeader"
          size="small"
          style="width: 100px"
          placeholder="负责人"
          @change="(val) => handleCheckUserIsLeader(val, record)"
          :options="isLeaderOptions"
        />
      </template>
    </BasicTable>
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, reactive, ref, unref, computed } from 'vue';
  import { Tag, Select } from 'ant-design-vue';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { BasicTable, useTable, TableActionType } from '/@/components/Table';
  import { getOrganizationUserPage } from '/@/api/user';
  import {
    getOrganizationUserIds,
    getOrganizationById,
    checkOrganizationHasLeader,
  } from '/@/api/organization';
  import { GetOrgizationModel } from '/@/api/organization/model/organizationModel';
  import { organizationUserColumns, searchFormSchema } from './organization.data';
  import { getPositionOptions } from '/@/views/authorization/position/position.data';
  import { useMessage } from '/@/hooks/web/useMessage';
  import { getIsLeaderOptions } from '../user/user.data';

  type OptionsItem = { label: string; value: string; disabled?: boolean };

  export default defineComponent({
    name: 'OrganizationUserDrawer',
    components: { BasicDrawer, BasicTable, Tag, Select },
    setup(_, { emit }) {
      const selectedOrganizationUserIds = ref<number[]>([]);
      const searchInfo = reactive<Recordable>({});
      const tableRef = ref<Nullable<TableActionType>>(null);
      const positionOptions = ref<OptionsItem[]>([]);
      const isLeaderOptions = ref<OptionsItem[]>([]);
      const { notification } = useMessage();
      const organizationInfo = ref<GetOrgizationModel>({});

      const getTitle = computed(() => `添加【${unref(organizationInfo).name}】成员`);

      const [registerTable, { reload, setSelectedRowKeys }] = useTable({
        rowKey: 'id',
        columns: organizationUserColumns,
        immediate: false,
        clickToRowSelect: false,
        api: getOrganizationUserPage,
        formConfig: {
          labelWidth: 120,
          schemas: searchFormSchema,
          autoSubmitOnEnter: true,
        },
        useSearchForm: true,
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
      const [registerDrawer, { closeDrawer, setDrawerProps }] = useDrawerInner(
        async (data: any) => {
          searchInfo.id = data.id;
          organizationInfo.value = await getOrganizationById(data.id);
          var organizationUserIds = await getOrganizationUserIds(data.id);
          selectedOrganizationUserIds.value = organizationUserIds;
          setSelectedRowKeys(organizationUserIds);
          positionOptions.value = await getPositionOptions(data.id, false);
          isLeaderOptions.value = getIsLeaderOptions();
          reload();
        },
      );

      async function handleSubmit() {
        try {
          setDrawerProps({ confirmLoading: true });
          const selectedRows = getTableAction().getSelectRows();
          const addUsers = selectedRows.map((item) => {
            if (!item.positionId) {
              notification.warning({
                message: `请先为用户${item.userName}选择职位信息`,
              });
              throw Error(`请先为用户${item.userName}选择职位信息`);
            }
            if (item.isLeader == null) {
              notification.warning({
                message: `请选择${item.userName}是否是部门负责人`,
              });
              throw Error(`请选择${item.userName}是否是部门负责人`);
            }
            return { userId: item.id, positionId: item.positionId, isLeader: item.isLeader };
          });

          closeDrawer();
          emit('success', { organizationUserId: searchInfo.id, addUsers });
        } finally {
          setDrawerProps({ confirmLoading: false });
        }
      }

      async function handleCheckUserIsLeader(value: any, record: any) {
        if (
          value &&
          (await checkOrganizationHasLeader(unref(organizationInfo).id, record.userId))
        ) {
          notification.warning({
            message: `${unref(organizationInfo).name}已经存在负责人`,
          });
          record.isLeader = null;
          throw Error(`${unref(organizationInfo).name}已经存在负责人`);
        }
      }

      return {
        registerTable,
        registerDrawer,
        handleSubmit,
        isCheckedUser,
        handleCheckUserIsLeader,
        searchInfo,
        tableRef,
        positionOptions,
        isLeaderOptions,
        getTitle,
      };
    },
  });
</script>

<style></style>
