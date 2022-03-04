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
    <BasicForm @register="registerForm" />
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref, computed, unref } from 'vue';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { getNameRules, getRealNameRules, roleSchemas } from './role.data';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';

  export default defineComponent({
    name: 'RoleDrawer',
    components: { BasicDrawer, BasicForm },
    setup(_, { emit }) {
      const isUpdate = ref(false);
      const roleId = ref<Nullable<number>>(null);
      const getTitle = computed(() => (!unref(isUpdate) ? '新增角色' : '编辑角色'));
      const [registerForm, { setFieldsValue, resetFields, validate, clearValidate, updateSchema }] =
        useForm({
          labelWidth: 100,
          schemas: roleSchemas,
          showActionButtonGroup: false,
        });

      const [registerDrawer, { setDrawerProps, closeDrawer }] = useDrawerInner(async (data) => {
        resetFields();
        isUpdate.value = !!data?.isUpdate;
        if (unref(isUpdate)) {
          setFieldsValue({
            ...data.record,
          });
          roleId.value = data.record.id;
          updateSchema({ field: 'name', rules: getNameRules(data.record.id) });
          updateSchema({ field: 'realName', rules: getRealNameRules(data.record.id) });
        } else {
          updateSchema({ field: 'name', rules: getNameRules(null) });
          updateSchema({ field: 'realName', rules: getRealNameRules(null) });
        }
        clearValidate();
        setDrawerProps({ confirmLoading: false });
      });

      async function handleSubmit() {
        try {
          setDrawerProps({ confirmLoading: true });
          const values = await validate();
          emit('success', { isUpdate: unref(isUpdate), values: { ...values, id: unref(roleId) } });
          closeDrawer();
        } finally {
          setDrawerProps({ confirmLoading: false });
        }
      }
      return {
        getTitle,
        registerForm,
        handleSubmit,
        registerDrawer,
      };
    },
  });
</script>
