<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    :title="getTitle"
    @ok="handleSubmit"
    showFooter
    width="30%"
    destroyOnClose
  >
    <BasicForm @register="registerForm" />
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref, computed, unref } from 'vue';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { organizationFormSchema } from './organization.data';
  import { getOrganizationById } from '/@/api/organization';

  export default defineComponent({
    name: 'OrganizationDrawer',
    components: { BasicDrawer, BasicForm },
    emits: ['success', 'register'],
    setup(_, { emit }) {
      const isUpdate = ref(true);
      const getTitle = computed(() => (!unref(isUpdate) ? '新增机构' : '编辑机构'));
      const id = ref<number | undefined>(undefined);
      const parentId = ref<number | undefined>(undefined);

      const [registerForm, { setFieldsValue, resetFields, validate }] = useForm({
        labelWidth: 100,
        schemas: organizationFormSchema,
        showActionButtonGroup: false,
        actionColOptions: {
          span: 23,
        },
      });

      const [registerDrawer, { setDrawerProps, closeDrawer }] = useDrawerInner(async (data) => {
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

      async function handleSubmit() {
        try {
          const values = await validate();
          setDrawerProps({ confirmLoading: true });
          closeDrawer();
          emit('success', {
            isUpdate: unref(isUpdate),
            values: { ...values, id: unref(id), parentId: unref(parentId) },
          });
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
