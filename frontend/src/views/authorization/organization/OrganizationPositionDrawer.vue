<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    showFooter
    :title="getTitle"
    width="600px"
    destroyOnClose
    @ok="handleSubmit"
  >
    <BasicForm @register="registerForm">
      <template #positionNamesSlot="{ model, field }">
        <Select
          v-model:value="model[field]"
          mode="multiple"
          allowClear
          placeholder="请选择要分配的职位"
        >
          <SelectOption
            v-for="(item, index) in positionOptions"
            :key="index"
            :disabled="item.disabled"
            :value="item.id"
          >
            <span aria-label="item.label">{{ item.label }}</span>
            <Tag color="blue" v-if="item.isPublic">公共</Tag>
          </SelectOption>
        </Select>
      </template>
    </BasicForm>
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref, unref } from 'vue';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { organizationPositionSchemas } from './organization.data';
  import { Tag, Select, SelectOption } from 'ant-design-vue';
  export default defineComponent({
    name: 'OrganizationPositionDrawer',
    components: { BasicDrawer, BasicForm, Tag, Select, SelectOption },
    setup(_, { emit }) {
      const getTitle = ref<string>('分配组织机构职位');
      const organizationId = ref<Nullable<number>>();
      const organizationName = ref<string>();
      const positionOptions = ref([]);
      const [registerDrawer, { setDrawerProps, closeDrawer }] = useDrawerInner(async (data) => {
        resetFields();
        clearValidate();
        organizationId.value = data.organizationId;
        organizationName.value = data.organizationName;
        getTitle.value = `分配【${unref(organizationName)}】职位`;
        positionOptions.value = data.positionOptions;
        setFieldsValue({
          ...data,
        });
        setDrawerProps({ confirmLoading: false });
      });

      const [registerForm, { setFieldsValue, resetFields, validate, clearValidate }] = useForm({
        labelWidth: 120,
        schemas: organizationPositionSchemas,
        showActionButtonGroup: false,
      });
      async function handleSubmit() {
        try {
          setDrawerProps({ confirmLoading: true });
          const values = await validate();
          emit('success', {
            id: unref(organizationId),
            organizationName: unref(organizationName),
            ...values,
          });
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
        positionOptions,
      };
    },
  });
</script>
