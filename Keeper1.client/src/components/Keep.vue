<template>
<div 
 class="card selectable text-white m-3" 
 data-bs-toggle="modal"
 data-bs-target="#active-keep"
 @click="setActive">
  <img v-if="keep.img" :src="keep.img" class="card-img object-fit-contain" :alt="keep.name">
  <div class="card-img-overlay">
    <h5 class="card-title">{{keep.name}}</h5>
  </div>
</div>
</template>


<script>
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Pop'
export default {
    props: {
        keep: {
            type: Object,
            required: true
        }
    },
    setup(props){
        return {
            async setActive(){
                try {
                    AppState.activeKeep = props.keep
                } catch (error) {
                    logger.error(error)
                    Pop.toast(error.message, 'error')
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped>
.keepImg{
    object-fit: cover;
    
}


</style>