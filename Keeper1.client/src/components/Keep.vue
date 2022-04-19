<template>
<div 
 class="card selectable text-white m-3" 
 data-bs-toggle="modal"
 data-bs-target="#active-keep"
 @click="setActive">
  <img v-if="keep.img" :src="keep.img" class="card-img object-fit-contain" :alt="keep.name">
  <div class="d-flex justify-content-between align-items-end card-img-overlay">
    <h4 class="card-title">{{keep.name}}</h4>
    <div>
    <img :src="keep.creator?.picture" class="rounded-circle profile-img" @click="goToProfile(keep.creator.id)">
        </div>
  </div>
</div>
</template>


<script>
import { useRouter } from 'vue-router'
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
        const router = useRouter()
        return {
            async setActive(){
                try {
                    AppState.activeKeep = props.keep
                } catch (error) {
                    logger.error(error)
                    Pop.toast(error.message, 'error')
                }
            },

            goToProfile(id){
                router.push({name: 'Profile', params: {id}})
            }
        }
    }
}
</script>


<style lang="scss" scoped>
.keepImg{
    object-fit: cover;
    
}

.profile-img{
    width: 35px;
    height: 35px;
}


</style>