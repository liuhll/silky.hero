<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    showFooter
    :title="getTitle"
    width="40%"
    @ok="handleSubmit"
  >
    <BasicForm @register="registerForm" />
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref, computed, unref } from 'vue';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { getTenantSchemas } from './tenant.data';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { useMessage } from '/@/hooks/web/useMessage';

  export default defineComponent({
    name: 'TenantDrawer',
    components: { BasicDrawer, BasicForm },
    setup(_, { emit }) {
      const isUpdate = ref(false);

      const tenantId = ref<Nullable<number>>(null);
      const getTitle = computed(() => (!unref(isUpdate) ? '新增租户' : '编辑租户'));
      const [registerForm, { setFieldsValue, resetFields, validate, clearValidate, setProps }] =
        useForm({
          labelWidth: 140,
          schemas: getTenantSchemas(unref(isUpdate)),
          showActionButtonGroup: false,
        });
      const { notification } = useMessage();

      const [registerDrawer, { setDrawerProps, closeDrawer }] = useDrawerInner(async (data) => {
        isUpdate.value = !!data?.isUpdate;
        setProps({ schemas: getTenantSchemas(unref(isUpdate)) });
        resetFields();
        clearValidate();
        if (unref(isUpdate)) {
          setFieldsValue({
            ...data.record,
          });
          tenantId.value = data.record.id;
        }
        setDrawerProps({ confirmLoading: false });
      });

      async function handleSubmit() {
        try {
          setDrawerProps({ confirmLoading: true });
          const values = await validate();
          emit('success', {
            isUpdate: unref(isUpdate),
            values: { ...values, id: unref(tenantId) },
          });
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
