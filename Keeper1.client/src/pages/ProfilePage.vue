<template>
    <div class="component container">
        <div class="row mt-4">
            <div class=" d-flex">
               <div> <img :src="profile.picture" alt=""/></div>
                <div class="ms-3 flex-column justify-content-center">
                    <h1>{{profile.name}}</h1>
                    <h6>Vaults: {{profileVaults.length}}</h6>
                    <h6>Keeps: {{profileKeeps.length}}</h6>
                    </div>
                    </div>
                    </div>
                   <h2 style="color:purple;" class="mt-5">Vaults
                    <i 
                    v-if="profile.id == account.id"
                   data-bs-toggle="modal"
                    data-bs-target="#create-vault"
                   class="mdi mdi-plus selectable"
                   title="Create Vault" >
                   
                   </i>
                   </h2>
                   <div class="row d-flex flex-row"> 
                    <div v-for="pv in profileVaults" :key="pv.id" class="col-md-4 p-1">
                    <div @click="goToVaultsPage(pv.id)" class="card shadow bg-primary selectable ">
                        <div class="card-body">
                            <i v-if="pv.isPrivate" class="mdi mdi-lock"></i>
                            {{pv.name}}
                        </div>
                    </div>
                    </div>
                </div> 
            
        
        <!-- <div class="masonry-with-columns">
            <div v-for="p in profileVaults" :key="p.id" >
                
            </div>
        </div> -->
            <h2 
            class="mt-4" style="color:purple;">Keeps 
                <i 
                v-if="profile.id == account.id"
                data-bs-toggle="modal"
                data-bs-target="#create-keep" 
                class="mdi mdi-plus selectable"
                title="Create Keep" >
                </i>
                </h2>
            <div class="masonry-with-columns">
                <!-- TODO Replace this with keep component -->
        <div v-for="pro in profileKeeps" :key="pro.id">
            <div 
            class="card shadow selectable text-white m-3">
            
            <img :src="pro.img" class="card-img object-fit-contain" :alt="pro.name">
            <div class="d-flex justify-content-between align-items-end card-img-overlay">
            <h4 class="card-title">{{pro.name}}</h4>
           </div>
        </div>
        </div>
        </div>
        <Modal id="create-keep">
            <template #modal-title> Create Keep </template>
            <template #modal-body><CreateKeepForm /> </template>
        </Modal>
        <Modal id="create-vault">
            <template #modal-title> Create Vault </template>
            <template #modal-body><CreateVaultForm /> </template>
        </Modal>
    </div>
      <link rel="stylesheet" href="//cdn.materialdesignicons.com/5.4.55/css/materialdesignicons.min.css">
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { AppState } from '../AppState'
import {profilesService} from '../services/ProfilesService.js'
import {vaultsService} from '../services/VaultsService.js'
import { router } from '../router'
export default {
    setup(){
        const route = useRoute()
        onMounted(async () => {
            try {
                await profilesService.getProfile(route.params.id)
                await profilesService.getVaults(route.params.id)
                await profilesService.getKeeps(route.params.id)
                
            } catch (error) {
                logger.error(error)
                Pop.toast(error.message, 'error')
            }
        })
        return {
            
            goToVaultsPage(id){
                router.push({name: 'Vault', params: {id}})
            },

            profile: computed(() => AppState.profile),
            keeps: computed(() => AppState.keeps),
            profileVaults: computed(() => AppState.profileVaults),
            profileKeeps: computed(()=> AppState.profileKeeps),
            account: computed(() => AppState.account),
            vaults: computed(() => AppState.vaults),

            
        }
    }
}
</script>


<style lang="scss" scoped>
.masonry-with-columns {
  columns: 6 200px;
  column-gap: 1rem;
  div {
    display: inline-block;
    width: 100%;
  }
}

</style>