<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    showFooter
    :title="getTitle"
    width="30%"
    @ok="handleSubmit"
  >
    <BasicForm @register="registerForm" />
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref, computed, unref } from 'vue';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { userRoleSchemas } from './user.data';
  export default defineComponent({
    name: 'UserRoleDrawer',
    components: { BasicDrawer, BasicForm },
    setup(_, { emit }) {
      const getTitle = computed(() => '授权用户角色');
      const userId = ref<Nullable<number>>();
      const userName = ref<string>();

      const [registerDrawer, { setDrawerProps, closeDrawer }] = useDrawerInner(async (data) => {
        resetFields();
        clearValidate();
        userId.value = data.userId;
        userName.value = data.userName;
        updateSchema({
          field: 'roleNames',
          componentProps: {
            options: data.roleOptions,
          },
        });
        setFieldsValue({
          ...data,
        });
        setDrawerProps({ confirmLoading: false });
      });

      const [registerForm, { setFieldsValue, resetFields, validate, clearValidate, updateSchema }] =
        useForm({
          labelWidth: 120,
          schemas: userRoleSchemas,
          showActionButtonGroup: false,
        });
      async function handleSubmit() {
        try {
          setDrawerProps({ confirmLoading: true });
          const values = await validate();
          emit('success', { id: unref(userId), userName: unref(userName), ...values });
          closeDrawer();
        } finally {
          setDrawerProps({ confirmLoading: false });
        }
      }
      return {
        getTitle,
        registerForm,
        registerDrawer,
        handleSubmit,
      };
    },
  });
</script>
