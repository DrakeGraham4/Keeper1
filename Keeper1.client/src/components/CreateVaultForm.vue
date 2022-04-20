<template>
    <form @submit.prevent="createVault">
    

        <div class="form-group flex-grow-1 p-2 m-2">

      <label for="title" class="">Name</label>
      <input type="text"
      v-model="vault.name"
        class="form-control" 
        name="name" 
        id="name" 
        required
        placeholder="Name...">
        </div>


        <div class="form-group flex-grow-1 p-2 m-2">
      <label for="title" class="">Description</label>
      <input type="text"
      v-model="vault.description"
        class="form-control" 
        name="description" 
        id="description" 
        required
        placeholder="Description...">
        </div>

        <div class="d-flex justify-content-end p-2">
       <button 
        type="submit"
        class="btn btn-success text-dark text-uppercase selectable">
        <b> Create Vault </b>
      </button>

        </div>

      
    

    
    </form>


    
</template>


<script>
import { ref } from '@vue/reactivity'
import { vaultsService } from '../services/VaultsService'
import { Modal } from 'bootstrap'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
export default {
    setup(){
        const vault = ref({})
        return {
            vault,
            async createVault(){
                try {
                    await vaultsService.createVault(vault.value)
                    Modal.getOrCreateInstance(document.getElementById('create-vault')).hide()
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

</style>