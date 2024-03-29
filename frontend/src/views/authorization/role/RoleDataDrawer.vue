<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    showFooter
    :title="getTitle"
    width="600px"
    @ok="handleSubmit"
    destroyOnClose
  >
    <BasicForm @register="registerForm">
      <template #dataRangeSlot="{ model, field }">
        <Select
          v-model:value="model[field]"
          placeholder="请选择"
          :options="DataRangeOptions"
          @change="handleChangeDataRange"
        />
      </template>
    </BasicForm>
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref, computed, unref } from 'vue';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { roleDataSchemas } from './role.data';
  import { Select } from 'ant-design-vue';
  import { DataRangeOptions, DataRange } from '/@/utils/dataRangeUtil';
  import { getOrganizationTreeList } from '/@/views/authorization/organization/organization.data';
  import { getRoleDataRange } from '/@/api/role';
  export default defineComponent({
    name: 'RoleDataDrawer',
    components: { BasicDrawer, BasicForm, Select },
    setup(_, { emit }) {
      const getTitle = computed(() => '授权角色数据权限');
      const roleId = ref<number>();

      const [registerDrawer, { setDrawerProps, closeDrawer }] = useDrawerInner(async (id) => {
        resetFields();
        clearValidate();
        const roleDataRange = await getRoleDataRange(id);
        const treeData = await getOrganizationTreeList(false);
        updateSchema({
          field: 'customOrganizationIds',
          componentProps: {
            'tree-data': treeData,
            defaultExpandAll: true,
          },
        });
        roleId.value = id;
        setFieldsValue({
          ...roleDataRange,
        });
        setDrawerProps({ confirmLoading: false });
      });

      function handleChangeDataRange(value: number) {
        if (value !== DataRange.SelfAndChildrenOrganization.valueOf()) {
          setFieldsValue({ customOrganizationIds: [] });
        }
      }

      const [registerForm, { setFieldsValue, resetFields, validate, clearValidate, updateSchema }] =
        useForm({
          labelWidth: 140,
          schemas: roleDataSchemas,
          showActionButtonGroup: false,
        });

      async function handleSubmit() {
        try {
          setDrawerProps({ confirmLoading: true });
          const values = await validate();
          emit('success', { ...values, id: unref(roleId) });
          closeDrawer();
        } finally {
          setDrawerProps({ confirmLoading: false });
        }
      }
      return {
        getTitle,
        DataRangeOptions,
        handleSubmit,
        registerForm,
        registerDrawer,
        handleChangeDataRange,
      };
    },
  });
</script>
