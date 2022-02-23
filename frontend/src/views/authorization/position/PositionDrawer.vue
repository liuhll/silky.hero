<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    showFooter
    :title="getTitle"
    width="500px"
    @ok="handleSubmit"
    destroyOnClose
  >
    <BasicForm @register="registerForm" />
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref, computed, unref } from 'vue';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { positionSchemas } from './position.data';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';

  export default defineComponent({
    name: 'PositionDrawer',
    components: { BasicDrawer, BasicForm },
    setup(_, { emit }) {
      const isUpdate = ref(false);
      const positionId = ref<Nullable<number>>(null);
      const getTitle = computed(() => (!unref(isUpdate) ? '新增角色' : '编辑角色'));
      const [registerForm, { setFieldsValue, resetFields, validate, clearValidate }] = useForm({
        labelWidth: 140,
        schemas: positionSchemas,
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
          positionId.value = data.record.id;
        }
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
