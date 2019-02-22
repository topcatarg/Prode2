<template>
  <div>
    <div class="vue-simple-spinner" :style="spinner_style"></div>
    <div class="vue-simple-spinner-text" :style="text_style" v-if="message.length > 0">{{message}}</div>
  </div>
</template>

<script lang="ts">

import { Component, Prop, Vue, Watch} from 'vue-property-decorator';

@Component
export default class Spinner extends Vue {
  @Prop({type: Number, default: 32}) public b!: number;
    @Prop(Number) public size!: number;
    @Prop(Number) public linesize!: number;
    @Prop(String) public linebgcolor: string = '#eee';
    @Prop(String) public linefgcolor: string = '#2196f3';
    @Prop(Number) public speed: number = 0.8;
    @Prop(Number) public spacing: number = 4;
    @Prop(String) public message: string = '';
    @Prop(Number) public fontsize: number = 13;
    @Prop(String) public textfgcolor: string = '#555';

    get Size(): number {
      if (this.size === null || this.size === undefined) {
        return 32;
      }
      return this.size;
    }

    get lineSize(): number {
      if (this.linesize === null || this.linesize === undefined) {
        return 3;
      }
      return this.linesize;

    }

    get spinner_style() {
        return {
          'margin': '0 auto',
          'border-radius': '100%',
          'border': this.lineSize + 'px solid ' + this.linebgcolor,
          'border-top': this.lineSize + 'px solid ' + this.linefgcolor,
          'width': this.Size + 'px',
          'height': this.Size + 'px',
          'animation': 'vue-simple-spinner-spin ' + this.speed + 's linear infinite'
        };
    }

    get text_style() {
        return {
          'margin-top': this.spacing + 'px',
          'color': this.textfgcolor,
          'font-size': this.fontsize + 'px',
          'text-align': 'center'
        };
    }

}

</script>

<style>
  .vue-simple-spinner {
    transition: all 0.3s linear;
  }

  @keyframes vue-simple-spinner-spin {
    0% { transform: rotate(0deg); }
    100% { transform: rotate(360deg); }
  }
</style>
