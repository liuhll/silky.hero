<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    showFooter
    :title="getTitle"
    width="40%"
    destroyOnClose
    @ok="handleSubmit"
  >
    <BasicForm @register="registerForm" />
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref, computed, unref } from 'vue';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { editionSchemas, getNameRules } from './edition.data';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { useMessage } from '/@/hooks/web/useMessage';

  export default defineComponent({
    name: 'EditionDrawer',
    components: { BasicDrawer, BasicForm },
    setup(_, { emit }) {
      const isUpdate = ref(false);

      const editionId = ref<Nullable<number>>(null);
      const getTitle = computed(() => (!unref(isUpdate) ? '新增版本' : '编辑版本'));
      const [registerForm, { setFieldsValue, resetFields, validate, clearValidate, updateSchema }] =
        useForm({
          labelWidth: 100,
          schemas: editionSchemas,
          showActionButtonGroup: false,
        });
      const { notification } = useMessage();

      const [registerDrawer, { setDrawerProps, closeDrawer }] = useDrawerInner(async (data) => {
        isUpdate.value = !!data?.isUpdate;
        resetFields();
        if (unref(isUpdate)) {
          setFieldsValue({
            ...data.record,
          });
          editionId.value = data.record.id;
          updateSchema({ field: 'name', rules: getNameRules(data.record.id) });
        } else {
          updateSchema({ field: 'name', rules: getNameRules(null) });
        }
        clearValidate();
        setDrawerProps({ confirmLoading: false });
      });

      async function handleSubmit() {
        try {
          setDrawerProps({ confirmLoading: true });
          const values = await validate();
          emit('success', {
            isUpdate: unref(isUpdate),
            values: { ...values, id: unref(editionId) },
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
