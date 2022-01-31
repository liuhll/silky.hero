<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    showFooter
    :title="getTitle"
    width="700px"
    @ok="handleSubmit"
  >
    <Card title="基础信息" :bordered="false">
      <BasicForm @register="registerForm" />
    </Card>
    <Card title="组织信息" :bordered="false">
      <UserSubsidiaryTable
        :user-subsidiary-data="userSubsidiariesList"
        :position-options="positionOptions"
        :organization-tree-list="organizaionTreeList"
        ref="userSubsidiaryTableRef"
      />
    </Card>
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref, computed, unref, onMounted } from 'vue';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { userSchemas } from './user.data';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { Card } from 'ant-design-vue';
  import { getPositionOptions } from '/@/views/authorization/position/position.data';
  import { getOrganizationTreeList } from '/@/views/authorization/organization/organization.data';
  import { OptionsItem } from '/@/utils/model';
  import UserSubsidiaryTable from './UserSubsidiaryTable.vue';
  import { TreeItem } from '/@/components/Tree';
  import { useMessage } from '/@/hooks/web/useMessage';

  export default defineComponent({
    name: 'UserDrawer',
    components: { BasicDrawer, BasicForm, UserSubsidiaryTable, Card },
    setup(_, { emit }) {
      const isUpdate = ref(false);
      const positionOptions = ref<OptionsItem[]>([]);
      const organizaionTreeList = ref<TreeItem[]>([]);
      const userSubsidiaryTableRef = ref<{
        getDataSource: () => any;
        setTableData: (data: any[]) => void;
      } | null>(null);
      const getTitle = computed(() => (!unref(isUpdate) ? '新增用户' : '编辑角色'));
      const [registerForm, { setFieldsValue, resetFields, validate, clearValidate }] = useForm({
        labelWidth: 140,
        schemas: userSchemas,
        showActionButtonGroup: false,
      });
      const { notification } = useMessage();

      onMounted(async () => {
        positionOptions.value = await getPositionOptions({});
        organizaionTreeList.value = await getOrganizationTreeList();
      });

      const [registerDrawer, { setDrawerProps, closeDrawer }] = useDrawerInner(async (data) => {
        resetFields();
        clearValidate();
        const isUpdate = !!data?.isUpdate;
        if (isUpdate) {
        } else {
          userSubsidiaryTableRef.value?.setTableData([]);
        }
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
            return { organizationId: item.organizationId, positionId: item.positionId };
          });
          values.userSubsidiaries = userSubsidiaries;
          emit('success', { ...values });
          closeDrawer();
        } finally {
          setDrawerProps({ confirmLoading: false });
        }
      }
      return {
        getTitle,
        positionOptions,
        organizaionTreeList,
        userSubsidiaryTableRef,
        registerForm,
        handleSubmit,
        registerDrawer,
      };
    },
  });
</script>
