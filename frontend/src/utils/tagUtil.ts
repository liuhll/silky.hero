import { Tag } from 'ant-design-vue';
import { h } from 'vue';

export const commonTagRender = (color: string, curVal: string) => h(Tag, { color }, () => curVal);

export const commonLinkRender = (href: string, text: string) => h('a', { href, target: '_blank' }, text);
