<template>
    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <h3>{{vault.name}}</h3>
                <button 
                @click="deleteVault" 
                v-if="vault.creatorId == account.id" 
                class="btn btn-danger">
                Delete Vault
                </button>
            </div>
        </div>
        <div class="masonry-with-columns">
            <div v-for="k in keeps" :key="k.id" >
                <Keep :keep="k" />
            </div>

        </div>


    </div>
</template>


<script>
import { computed, onMounted } from '@vue/runtime-core'
import Keep from '../components/Keep.vue'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { profilesService } from '../services/ProfilesService'
import { AppState } from '../AppState'
export default {
    setup(){
        onMounted(async () => {
            try {
                await profilesService.getVaults(route.params.id)
                await profilesService.getKeeps(route.params.id)
            } catch (error) {
                logger.log(error)
                Pop.toast(error.message, 'error')
            }
        })
        return {
            vault: computed(() => AppState.vaults),
            keep: computed(() => AppState.keeps)
        }
    }
}
</script>


<style lang="scss" scoped>

</style>