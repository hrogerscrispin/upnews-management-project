import { Status } from "../models/status_schema.js";

export const getAllStatuses = async(res)=>{
    try{

        const estados = await Status.find();

        res.status(200).json({
            success: true,
            message: 'Estados encontrados correctamente',
            data: estados
        });

    }catch(error){
        console.error("Error al obtener estados", error);
        res.status(500).json({
            success:false,
            message:'No se pudieron encontrar los estados',
            error: error.message
        })
    }
}

// todo: create function getStatusesById 