<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    showFooter
    :title="getTitle"
    width="500px"
    @ok="handleSubmit"
  >
    <BasicForm @register="registerForm" />
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref, computed, unref, onMounted } from 'vue';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { roleSchemas } from './role.data';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { Card } from 'ant-design-vue';

  export default defineComponent({
    name: 'roleDrawer',
    components: { BasicDrawer, BasicForm, Card },
    setup(_, { emit }) {
      const isUpdate = ref(false);
      const roleId = ref<Nullable<number>>(undefined);
      const getTitle = computed(() => (!unref(isUpdate) ? '新增角色' : '编辑角色'));
      const [registerForm, { setFieldsValue, resetFields, validate, clearValidate, updateSchema }] =
        useForm({
          labelWidth: 140,
          schemas: roleSchemas,
          showActionButtonGroup: false,
        });

      const [registerDrawer, { setDrawerProps, closeDrawer }] = useDrawerInner(async (data) => {
        resetFields();
        clearValidate();
        isUpdate.value = !!data?.isUpdate;
        if (unref(isUpdate)) {
          setFieldsValue({
            ...data.record,
          });
          roleId.value = data.record.id;
        }
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
