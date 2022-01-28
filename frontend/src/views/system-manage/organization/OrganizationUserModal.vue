<template>
  <BasicModal
    v-bind="$attrs"
    @register="registerModal"
    title="新增用户"
    @ok="handleSubmit"
    :width="800"
  >
    <BasicTable @register="registerTable" :searchInfo="searchInfo" :maxHeight="400" />
  </BasicModal>
</template>

<script lang="ts">
import { defineComponent, reactive, ref, computed, unref } from 'vue';
import { BasicModal, useModalInner } from '/@/components/Modal';
import { BasicTable, useTable, TableAction, TableActionType } from '/@/components/Table';
import { getUserPageList } from '/@/api/user';
import { getOrganizationUserIds } from '/@/api/organization';
import { organizationUserColumns } from './organization.data';
export default defineComponent({
  name: 'OrganizationUserModal',
  components: { BasicModal, BasicTable },
  setup(_, { emit }) {
    const searchInfo = reactive<Recordable>({});
    const [registerTable, { reload, setSelectedRowKeys }] = useTable({
      rowKey: 'id',
      columns: organizationUserColumns,
      immediate: false,
      api: getUserPageList,
      rowSelection: {
        type: 'checkbox',
      },
    });
    const [registerModal] = useModalInner(async (data) => {
      searchInfo.id = data.id;
      var organizationUserIds = await getOrganizationUserIds(data.id);
      setSelectedRowKeys(organizationUserIds);
      reload();
    });

    return {
      registerTable,
      registerModal,
      searchInfo,
    };
  },
});
</script>

<style>
</style>
