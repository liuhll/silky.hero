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
            :value="item.value"
          >
            <span>{{ item.label }}</span>
            <Tag color="blue" v-if="item.isPublic">公共</Tag>
            <Tag color="green" v-if="item.isDefault">默认</Tag>
            <!-- <Tag color="cyan" v-if="item.isStatic" style="margin-left: 3px">静态</Tag> -->
          </SelectOption>
        </Select>
      </template>
    </BasicForm>
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref, computed, unref } from 'vue';
  import { BasicForm, useForm } from '/@/components/Form/index';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { userRoleSchemas } from './user.data';
  import { Tag, Select, SelectOption } from 'ant-design-vue';
  export default defineComponent({
    name: 'UserRoleDrawer',
    components: { BasicDrawer, BasicForm, Tag, Select, SelectOption },
    setup(_, { emit }) {
      const getTitle = computed(() => '授权用户角色');
      const userId = ref<Nullable<number>>();
      const userName = ref<string>();
      const roleOptions = ref([]);

      const [registerDrawer, { setDrawerProps, closeDrawer }] = useDrawerInner(async (data) => {
        resetFields();
        clearValidate();
        userId.value = data.userId;
        userName.value = data.userName;
        roleOptions.value = data.roleOptions;
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
        roleOptions,
        registerForm,
        registerDrawer,
        handleSubmit,
      };
    },
  });
</script>
