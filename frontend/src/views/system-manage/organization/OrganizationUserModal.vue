<template>
  <BasicModal
    v-bind="$attrs"
    @register="registerModal"
    title="新增用户"
    @ok="handleSubmit"
    :width="800"
  >
    <BasicTable @register="registerTable" />
  </BasicModal>
</template>

<script lang="ts">
import { defineComponent, ref, computed, unref } from 'vue';
import { BasicModal, useModalInner } from '/@/components/Modal';
import { BasicTable, useTable, TableAction, TableActionType } from '/@/components/Table';
import { organizationUserColumns } from './organization.data';
export default defineComponent({
  name: 'OrganizationUserModal',
  components: { BasicModal, BasicTable },
  setup(_, { emit }) {
    const [registerTable, { reload, updateTableDataRecord }] = useTable({
      rowKey: 'id',
      columns: organizationUserColumns,
      immediate: false,
    });
    const [registerModal, { closeModal }] = useModalInner(async (data) => {});

    return {
      registerTable,
      registerModal,
    };
  },
});
</script>

<style>
</style>
