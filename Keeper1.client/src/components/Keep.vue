<template>
<div class="card selectable text-white m-3">
  <img v-if="keep.img" :src="keep.img" class="card-img object-fit-contain" :alt="keep.name">
  <div class="card-img-overlay">
    <h5 class="card-title">{{keep.name}}</h5>
  </div>
</div>
</template>


<script>
export default {
    props: {
        keep: {
            type: Object,
            required: true
        }
    },
    setup(){
        return {}
    }
}
</script>


<style lang="scss" scoped>
.keepImg{
    object-fit: cover;
    
}


</style>