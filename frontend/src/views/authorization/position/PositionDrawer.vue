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
  import { getNameRules, positionSchemas } from './position.data';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';

  export default defineComponent({
    name: 'PositionDrawer',
    components: { BasicDrawer, BasicForm },
    setup(_, { emit }) {
      const isUpdate = ref(false);
      const positionId = ref<Nullable<number>>(null);
      const getTitle = computed(() => (!unref(isUpdate) ? '新增岗位' : '编辑岗位'));
      const [registerForm, { setFieldsValue, resetFields, validate, clearValidate, updateSchema }] =
        useForm({
          labelWidth: 100,
          schemas: positionSchemas,
          showActionButtonGroup: false,
        });

      const [registerDrawer, { setDrawerProps, closeDrawer }] = useDrawerInner(async (data) => {
        resetFields();
        isUpdate.value = !!data?.isUpdate;
        if (unref(isUpdate)) {
          setFieldsValue({
            ...data.record,
          });
          positionId.value = data.record.id;
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
            values: { ...values, id: unref(positionId) },
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
