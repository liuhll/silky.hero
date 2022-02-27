<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    showFooter
    :title="getTitle"
    width="50%"
    destroyOnClose
    @ok="handleSubmit"
  >
    <BasicForm @register="registerForm" />
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref, computed, unref } from 'vue';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { menuSchemas, getMenuTreeList, getNameRules } from './menu.data';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';

  export default defineComponent({
    name: 'MenuDrawer',
    components: { BasicDrawer, BasicForm },
    setup(_, { emit }) {
      const isUpdate = ref(false);
      const menuId = ref<Nullable<number>>(null);
      const getTitle = computed(() => (!unref(isUpdate) ? '新增菜单' : '编辑菜单'));
      const [
        registerForm,
        { setFieldsValue, resetFields, validate, clearValidate, updateSchema, getFieldsValue },
      ] = useForm({
        labelWidth: 100,
        schemas: menuSchemas,
        showActionButtonGroup: false,
        baseColProps: { lg: 12, md: 24 },
      });

      const [registerDrawer, { setDrawerProps, closeDrawer }] = useDrawerInner(async (data) => {
        resetFields();
        const treeData = await getMenuTreeList({});
        updateSchema({
          field: 'parentId',
          componentProps: { treeData },
        });
        isUpdate.value = !!data?.isUpdate;
        if (unref(isUpdate)) {
          setFieldsValue({
            ...data.record,
          });
          menuId.value = data.record.id;
          updateSchema({
            field: 'name',
            rules: getNameRules(unref(menuId), data.record.parentId),
          });
        }
        clearValidate();
        setDrawerProps({ confirmLoading: false });
      });

      async function handleSubmit() {
        try {
          setDrawerProps({ confirmLoading: true });
          updateSchema({
            field: 'name',
            rules: getNameRules(unref(menuId), getFieldsValue().parentId),
          });
          const values = await validate();
          emit('success', { isUpdate: unref(isUpdate), values: { ...values, id: unref(menuId) } });
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
