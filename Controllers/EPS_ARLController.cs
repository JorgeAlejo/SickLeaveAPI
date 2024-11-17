using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class EPS_ARLController : ControllerBase{
    private readonly EPS_ARLService _epsArlService;

    public EPS_ARLController(EPS_ARLService epsArlService){
        _epsArlService = epsArlService;
    }

    // Crear una nueva EPS/ARL
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] EPS_ARL epsArl){
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var result = await _epsArlService.Create(epsArl);
        if (!result) return Conflict(new { message = "An EPS/ARL with the same email already exists." });

        return CreatedAtAction(nameof(GetById), new { id = epsArl.Id }, epsArl);
    }

    // Obtener todas las EPS/ARL
    [HttpGet]
    public async Task<IActionResult> GetAll(){
        var epsArls = await _epsArlService.GetAll();
        return Ok(epsArls);
    }

    // Obtener una EPS/ARL por ID
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id){
        var epsArl = await _epsArlService.GetById(id);
        if (epsArl == null) return NotFound(new { message = "EPS/ARL not found." });
        
        return Ok(epsArl);
    }

    // Actualizar una EPS/ARL
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] EPS_ARL updatedEpsArl){
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var result = await _epsArlService.Update(id, updatedEpsArl);
        if (!result) return NotFound(new { message = "EPS/ARL not found." });
        
        return Ok(new { message = "EPS/ARL updated successfully." });
    }

    // Eliminar una EPS/ARL
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id){
        var result = await _epsArlService.Delete(id);
        if (!result) return NotFound(new { message = "EPS/ARL not found." });
        
        return Ok(new { message = "EPS/ARL deleted successfully." });
    }
}
