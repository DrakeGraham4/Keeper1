<template>
<div 
 class="card selectable text-white m-2" 
 data-bs-toggle="modal"
 data-bs-target="#active-keep"
 @click="setActive">
  <img v-if="keep.img" :src="keep.img" class="card-img object-fit-contain" :alt="keep.name">
  
  <div class="d-flex justify-content-between align-items-end card-img-overlay">
    <h4 class="card-title">{{keep.name}}</h4>
    <i @click="deleteVaultKeep(keep.vaultKeepId)" v-if="keep.vaultKeepId !== undefined && activeVault.creatorId == account.id" class="mdi mdi-trash-can"></i>
    <div>
    <img :src="keep.creator?.picture" class="rounded-circle profile-img" @click="goToProfile(keep.creator.id)">
        </div>
  </div>
</div>
</template>


<script>
import { computed } from '@vue/reactivity'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { Modal } from 'bootstrap'
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
            account: computed(()=> AppState.account),
            vaultKeep: computed(() => AppState.vaultKeeps),
            activeVault: computed(() => AppState.activeVault),

            async setActive(){
                try {
                    AppState.activeKeep = props.keep
                    await keepsService.getById(props.keep.id)

                } catch (error) {
                    logger.error(error)
                    Pop.toast(error.message, 'error')
                }
            },

            goToProfile(id){
                router.push({name: 'Profile', params: {id}})
            },

            async deleteVaultKeep(id){
                try {
                    await vaultsService.deleteVaultKeep(id)
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

.profile-img{
    width: 35px;
    height: 35px;
}


</style>