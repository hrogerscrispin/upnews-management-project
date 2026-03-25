import { Country } from "../models/country_schema.js";

// Crear un nuevo país
// export const createCountry = async (req, res) => {
//   try {
//     const { nombrePais } = req.body;

//     if (!nombrePais) {
//       return res.status(400).json({
//         success: false,
//         message: 'El nombre del país es requerido'
//       });
//     }

//     const nuevoPais = new Country({ nombrePais });
//     await nuevoPais.save();

//     res.status(201).json({
//       success: true,
//       message: 'País creado correctamente',
//       data: nuevoPais
//     });
//   } catch (error) {
//     console.error('Error al crear país:', error);
//     res.status(500).json({
//       success: false,
//       message: 'Error al crear país',
//       error: error.message
//     });
//   }
// };

// Obtener todos los países
export const getAllCountries = async (req, res) => {
  try {
    const paises = await Country.find();

    res.status(200).json({
      success: true,
      message: 'Países obtenidos correctamente',
      data: paises
    });
  } catch (error) {
    console.error('Error al obtener países:', error);
    res.status(500).json({
      success: false,
      message: 'Error al obtener países',
      error: error.message
    });
  }
};

// Obtener país por ID
export const getCountryById = async (req, res) => {
  try {
    const { id } = req.params;
    const pais = await Country.findById(id);

    if (!pais) {
      return res.status(404).json({
        success: false,
        message: 'País no encontrado'
      });
    }

    res.status(200).json({
      success: true,
      message: 'País obtenido correctamente',
      data: pais
    });
  } catch (error) {
    console.error('Error al obtener país:', error);
    res.status(500).json({
      success: false,
      message: 'Error al obtener país',
      error: error.message
    });
  }
};
