<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    showFooter
    :title="getTitle"
    width="40%"
    @ok="handleSubmit"
  >
    <Form
      :model="formState"
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 14 }"
      @submit="handleSubmit"
    >
      <Tabs tab-position="left">
        <TabPane
          v-for="(editionFeatureCatalog, index) in editionFeature?.featureCatalogs"
          :key="`${index}`"
          :tab="`${editionFeatureCatalog.name}`"
        >
          <Divider orientation="left">{{ editionFeatureCatalog.name }}</Divider>
          <FormItem
            v-for="(feature, index) in editionFeatureCatalog.features"
            :key="index"
            :label="feature.name"
          >
            <InputNumber
              v-if="feature.featureType === 0"
              style="width: 100%"
              v-model:value="formState[feature.featureId]"
            />
            <Switch
              v-if="feature.featureType === 1"
              v-model:checked="formState[feature.featureId]"
            />
            <Select v-if="feature.featureType === 2" v-model:value="feature.featureValue" />
          </FormItem>
        </TabPane>
      </Tabs>
    </Form>
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref, unref } from 'vue';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import {
    Tabs,
    TabPane,
    Divider,
    Form,
    FormItem,
    Switch,
    InputNumber,
    Select,
  } from 'ant-design-vue';
  import { getEditionById } from '/@/api/edition';
  import { GetEditionFeatureModel, GetFeatureModel } from '/@/api/edition/model/editionModel';
  import { debug } from 'console';
  import { number } from 'echarts';
  export default defineComponent({
    name: 'EditionFeatureDrawer',
    components: {
      BasicDrawer,
      Form,
      FormItem,
      Tabs,
      TabPane,
      Divider,
      Switch,
      InputNumber,
      Select,
    },
    setup(_, { emit }) {
      const getTitle = ref<string>('设置版本功能');
      const editionId = ref<number>();
      const editionFeature = ref<GetEditionFeatureModel>();
      const formState = ref({});

      const [registerDrawer, { setDrawerProps, closeDrawer }] = useDrawerInner(async (id) => {
        editionFeature.value = await getEditionById(id);
        getTitle.value = unref(editionFeature)?.name;
        editionId.value = id;
        setFormState();
        setDrawerProps({ confirmLoading: false });
      });

      function setFormState() {
        for (const featureCatalog of unref(editionFeature)?.featureCatalogs) {
          for (const feature of featureCatalog.features) {
            if (feature.featureType === 1) {
              formState.value[feature.featureId] = Boolean(feature.featureValue);
            } else {
              formState.value[feature.featureId] = feature.featureValue;
            }
          }
        }
      }

      async function handleSubmit() {
        try {
          setDrawerProps({ confirmLoading: true });
          const features: any[] = [];
          for (let k in unref(formState)) {
            const featureItem = {
              featureId: Number(k),
              featureValue: Number((unref(formState) as any)[k]),
            };
            features.push(featureItem);
          }
          emit('success', { id: unref(editionId), name: unref(getTitle), features: features });
          closeDrawer();
        } finally {
          setDrawerProps({ confirmLoading: false });
        }
      }
      return {
        getTitle,
        editionFeature,
        handleSubmit,
        registerDrawer,
        formState,
      };
    },
  });
</script>
