<template>
  <BasicModal
    v-bind="$attrs"
    @register="registerModal"
    showFooter
    useWrapper
    :canFullscreen="false"
    :title="getTitle"
    :min-height="30"
    width="400px"
    @ok="handleSubmit"
  >
    <BasicForm @register="registerForm" />
  </BasicModal>
</template>

<script lang="ts">
  import { defineComponent, ref, computed, unref } from 'vue';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { userLockSchemas } from './user.data';
  export default defineComponent({
    name: 'UserLockModal',
    components: { BasicModal, BasicForm },
    setup(_, { emit }) {
      const getTitle = computed(() => `锁定用户：${unref(realName)}`);
      const userId = ref<Nullable<number>>();
      const realName = ref<string>();

      const [registerModal, { setModalProps, closeModal }] = useModalInner(async (data) => {
        resetFields();
        clearValidate();
        userId.value = data.userId;
        realName.value = data.realName;
        setFieldsValue({
          ...data,
        });
        setModalProps({ confirmLoading: false });
      });

      const [registerForm, { setFieldsValue, resetFields, validate, clearValidate }] = useForm({
        labelWidth: 120,
        schemas: userLockSchemas,
        showActionButtonGroup: false,
      });
      async function handleSubmit() {
        try {
          setModalProps({ confirmLoading: true });
          const values = await validate();
          emit('success', { id: unref(userId), realName: unref(realName), ...values });
          closeModal();
        } finally {
          setModalProps({ confirmLoading: false });
        }
      }
      return {
        getTitle,
        registerForm,
        registerModal,
        handleSubmit,
      };
    },
  });
</script>
