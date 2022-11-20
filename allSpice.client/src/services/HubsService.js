// import { baseURL, useSignalR } from "../env";
// import { logger } from "../utils/Logger"

// "use strict";

// const connection = new signalR.HubConnectionBuilder().withUrl(baseURL + "/Hubs/UpdatesHub").withAutomaticReconnect().build()


  class HubsService{
  
  // constructor(requiresAuth = false, url){
  //   if (!useSignalR) {
  //     return
  //   }
  //   this.requiresAuth = requiresAuth
  // }
  // on(event, fn){
  //   this.connection
  // }
//   async start(){
//     try {
//        const res = connection.start()
//        console.log(res);
       
//       } catch (error) {
//         logger.log(error)
//       }
//   }
  
 }
 export const hubsService = new HubsService()