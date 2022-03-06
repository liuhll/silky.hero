<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    showFooter
    :title="getTitle"
    width="800px"
    destroyOnClose
    @ok="handleSubmit"
  >
    <Card title="基础信息" :bordered="false">
      <BasicForm @register="registerForm" />
    </Card>
    <Card title="组织信息" :bordered="false">
      <UserSubsidiaryTable ref="userSubsidiaryTableRef" />
    </Card>
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref, computed, unref } from 'vue';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import {
    getMobilePhoneRules,
    getEmailRules,
    getUserNameRules,
    userSchemas,
    getJobNumberRules,
  } from './user.data';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { Card } from 'ant-design-vue';
  import UserSubsidiaryTable from './UserSubsidiaryTable.vue';
  import { useMessage } from '/@/hooks/web/useMessage';

  export default defineComponent({
    name: 'UserDrawer',
    components: { BasicDrawer, BasicForm, UserSubsidiaryTable, Card },
    setup(_, { emit }) {
      const isUpdate = ref(false);

      const userId = ref<Nullable<number>>(null);
      const userSubsidiaryTableRef = ref<{
        getDataSource: () => any;
        setTableData: (data: any[]) => void;
        setOrganizaionTreeList: () => void;
        setPositionOptions: (id: Nullable<number>) => void;
        setIsLeaderOptions: () => void;
      } | null>(null);
      const getTitle = computed(() => (!unref(isUpdate) ? '新增用户' : '编辑用户'));
      const [registerForm, { setFieldsValue, resetFields, validate, clearValidate, updateSchema }] =
        useForm({
          labelWidth: 130,
          schemas: userSchemas,
          showActionButtonGroup: false,
        });
      const { notification } = useMessage();

      const [registerDrawer, { setDrawerProps, closeDrawer }] = useDrawerInner(async (data) => {
        resetFields();
        await userSubsidiaryTableRef.value?.setOrganizaionTreeList();
        await userSubsidiaryTableRef.value?.setPositionOptions(null);
        await userSubsidiaryTableRef.value?.setIsLeaderOptions();
        isUpdate.value = !!data?.isUpdate;
        if (unref(isUpdate)) {
          setFieldsValue({
            ...data.record,
          });
          userId.value = data.record.id;
          updateSchema({ field: 'password', show: false });
          userSubsidiaryTableRef.value?.setTableData(data.record.userSubsidiaries);
          updateSchema({ field: 'userName', rules: getUserNameRules(data.record.id) });
          updateSchema({ field: 'email', rules: getEmailRules(data.record.id) });
          updateSchema({ field: 'mobilePhone', rules: getMobilePhoneRules(data.record.id) });
          updateSchema({ field: 'jobNumber', rules: getJobNumberRules(data.record.id) });
        } else {
          updateSchema({ field: 'password', show: true });
          userSubsidiaryTableRef.value?.setTableData([]);
          updateSchema({ field: 'userName', rules: getUserNameRules(null) });
          updateSchema({ field: 'email', rules: getEmailRules(null) });
          updateSchema({ field: 'mobilePhone', rules: getMobilePhoneRules(null) });
          updateSchema({ field: 'jobNumber', rules: getJobNumberRules(data.record.id) });
        }
        clearValidate();
        setDrawerProps({ confirmLoading: false });
      });

      async function handleSubmit() {
        try {
          setDrawerProps({ confirmLoading: true });
          const data = userSubsidiaryTableRef.value?.getDataSource();

          const editRows = data.filter((item) => item.editable === true);
          if (editRows.length > 0) {
            notification.error({
              message: '请先保存用户组织信息',
            });
            throw Error('请先保存用户组织信息');
          }
          const values = await validate();
          const userSubsidiaries = data.map((item) => {
            return {
              organizationId: item.organizationId,
              positionId: item.positionId,
              isLeader: item.isLeader,
            };
          });
          values.userSubsidiaries = userSubsidiaries;
          emit('success', { isUpdate: unref(isUpdate), values: { ...values, id: unref(userId) } });
          closeDrawer();
        } finally {
          setDrawerProps({ confirmLoading: false });
        }
      }
      return {
        getTitle,
        userSubsidiaryTableRef,
        registerForm,
        handleSubmit,
        registerDrawer,
      };
    },
  });
</script>

function getEmailRules(id: any): import("/@/components/Form/index").Rule[]|undefined { throw new
Error('Function not implemented.'); } function getEmailRules(id: any):
import("/@/components/Form/index").Rule[]|undefined { throw new Error('Function not implemented.');
}
