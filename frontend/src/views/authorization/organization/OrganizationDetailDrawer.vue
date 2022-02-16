<template>
  <BasicDrawer v-bind="$attrs" @register="registerDrawer" :title="getTitle" width="600px">
    <Description
      size="middle"
      :column="2"
      :data="organizationDetail"
      :schema="organizationDetailSchemas"
      class="enter-y"
    />
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref } from 'vue';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { Description } from '/@/components/Description/index';
  import { getOrganizationById } from '/@/api/organization';
  import { organizationDetailSchemas } from './organization.data';

  export default defineComponent({
    name: 'OrganizationDetailDrawer',
    components: { BasicDrawer, Description },
    setup() {
      const getTitle = ref('职位');
      const organizationDetail = ref();

      const [registerDrawer] = useDrawerInner(async (id) => {
        const data = await getOrganizationById(id);
        getTitle.value = data.name;
        organizationDetail.value = data;
      });
      return {
        getTitle,
        organizationDetail,
        organizationDetailSchemas,
        registerDrawer,
      };
    },
  });
</script>
