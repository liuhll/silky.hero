<template>
  <BasicModal v-bind="$attrs" @register="registerModal" :title="getTitle" @ok="handleSubmit">
    <BasicForm @register="registerForm" />
  </BasicModal>
</template>

<script lang="ts">
  import { defineComponent, ref, computed, unref } from 'vue';
  import { BasicModal, useModalInner } from '/@/components/Modal';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { organizationFormSchema } from './organization.data';
  import { getOrganizationById } from '/@/api/organization';

  export default defineComponent({
    name: 'OrganizationModal',
    components: { BasicModal, BasicForm },
    emits: ['success', 'register'],
    setup (_, { emit }) {
      const isUpdate = ref(true);
      const getTitle = computed(() => (!unref(isUpdate) ? '新增机构' : '编辑机构'));
      const id = ref<number | undefined>(undefined);
      const parentId = ref<number | undefined>(undefined);

      const [registerForm, { setFieldsValue, updateSchema, resetFields, validate }] = useForm({
        labelWidth: 100,
        schemas: organizationFormSchema,
        showActionButtonGroup: false,
        actionColOptions: {
          span: 23,
        },
      });

      const [registerModal, { setModalProps, closeModal }] = useModalInner(async data => {
        resetFields();
        isUpdate.value = !!data?.isUpdate;
        if (unref(isUpdate)) {
          const organizationModel = await getOrganizationById(data.id);
          id.value = organizationModel.id;
          parentId.value = organizationModel.parentId;
          setFieldsValue({
            ...organizationModel,
          });
        } else {
          parentId.value = data.id;
        }
      });

      async function handleSubmit () {
        try {
          const values = await validate();
          setModalProps({ confirmLoading: true });
          closeModal();
          emit('success', {
            isUpdate: unref(isUpdate),
            values: { ...values, id: unref(id), parentId: unref(parentId) },
          });
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
