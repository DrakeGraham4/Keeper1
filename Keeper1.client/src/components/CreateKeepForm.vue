<template>
<form @submit.prevent="createKeep">
    

        <div class="form-group flex-grow-1 p-2 m-2">

      <label for="title" class="">Name</label>
      <input type="text"
      v-model="keep.name"
        class="form-control" 
        name="title" 
        id="title" 
        required
        placeholder="Name">
    </div>

    <div class="form-group flex-grow-1 p-2 m-2">
      <label for="imgUrl" class="">Image Url</label>
      <input type="text"
      v-model="keep.img"
        class="form-control" 
        name="imgUrl" 
        id="imgUrl" 
        required
        placeholder="Url...">
    </div>

        <div class="form-group flex-grow-1 p-2 m-2">
      <label for="description" class="form-label">Description</label>
      <input type="text"
        v-model="keep.description"
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
        <b> Create Keep </b>
      </button>

        </div>

      
    

    
    </form>
</template>


<script>
import { Modal } from 'bootstrap'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { computed, ref } from '@vue/reactivity'
import { AppState } from '../AppState'
export default {
    setup(){
        
        const keep = ref({})
        return {
            keep,
            async createKeep(){
                try {
                    logger.log(keep.value)
                    await keepsService.createKeep(keep.value)
                    Modal.getOrCreateInstance(document.getElementById('create-keep')).hide()
                } catch (error) {
                    logger.error(error)
                    Pop.toast(error.message, 'error')
                }
            },
            
        }
    }
}
</script>


<style lang="scss" scoped>

</style>