<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    showFooter
    :title="getTitle"
    width="30%"
    destroyOnClose
    @ok="handleSubmit"
  >
    <BasicForm @register="registerForm">
      <template #roleNamesSlot="{ model, field }">
        <Select
          v-model:value="model[field]"
          mode="multiple"
          allowClear
          placeholder="请选择要分配的角色"
        >
          <SelectOption
            v-for="(item, index) in roleOptions"
            :key="index"
            :disabled="item.disabled"
            :value="item.id"
          >
            <span role="img" aria-label="China">{{ item.label }}</span>
            <Tag color="green" v-if="item.isDefault">默认</Tag>
            <!-- <Tag color="cyan" v-if="item.isStatic" style="margin-left: 3px">静态</Tag> -->
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
  import { organizationRoleSchemas } from './organization.data';
  import { Tag, Select, SelectOption } from 'ant-design-vue';
  export default defineComponent({
    name: 'OrganizationRoleDrawer',
    components: { BasicDrawer, BasicForm, Tag, Select, SelectOption },
    setup(_, { emit }) {
      const getTitle = ref<string>('分配组织机构角色');
      const organizationId = ref<Nullable<number>>();
      const organizationName = ref<string>();
      const roleOptions = ref([]);
      const [registerDrawer, { setDrawerProps, closeDrawer }] = useDrawerInner(async (data) => {
        resetFields();
        clearValidate();
        organizationId.value = data.organizationId;
        organizationName.value = data.organizationName;
        getTitle.value = `分配${unref(organizationName)}机构角色`;
        roleOptions.value = data.roleOptions;
        setFieldsValue({
          ...data,
        });
        setDrawerProps({ confirmLoading: false });
      });

      const [registerForm, { setFieldsValue, resetFields, validate, clearValidate, updateSchema }] =
        useForm({
          labelWidth: 120,
          schemas: organizationRoleSchemas,
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
        roleOptions,
      };
    },
  });
</script>
