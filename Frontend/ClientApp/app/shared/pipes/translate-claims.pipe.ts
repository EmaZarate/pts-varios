import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'translateClaims'
})
export class TranslateClaimsPipe implements PipeTransform {

  transform(value: any, localization: string): any {
    var splitedString = value.split('.');
    
    var moduleName = this.getTranslatedWordToSpanish(splitedString[0]);
    var claimName = [];

    if(localization == 'es'){
      splitedString.forEach((element,index) => {
        if(index != 0){
          claimName.push(this.getTranslatedWordToSpanish(element));
        }
      });
    }

    //return {module: moduleName, name: claimName.join(' ')};
    if(claimName.length > 0){
      return claimName.join(' ');
    }
    else{
      return moduleName;
    }
    
  }


  private getTranslatedWordToSpanish(word: String) {
    switch (word.toLowerCase()) {
      case "findings":
        return "Hallazgos";
      case "auditstates":
        return "Estados de Auditoría";
      case "audits":
        return "Auditorias";
      case "readcalendar":
        return "Ver el Calendario";
      case "schedule":
        return "Programar";
      case "reschedule":
        return "Reprogramar";
      case "planning":
        return "Planificar";
      case "emmit":
        return "Emitir";
      case "report":
        return "Reporte";
      case "rejectedreport":
        return "Reporte Rechazado";
      case "export":
        return "Exportar";
      case "addfindings":
        return "Agregar Hallazgos";
      case "aspects":
        return "Aspectos";
      case "standards":
        return "Normas";
      case "aspectstate":
        return "Estado de Aspectos";
      case "approve":
        return "Aprobar";
      case "approved":
        return "Aprobado";
      case "update":
        return "Editar";
      case "add":
        return "Crear";
      case "quality":
        return "Calidad";
      case "core":
        return "General";
      case "reject":
        return "Rechazar";
      case "reassign":
        return "Reasignar";
      case "direct":
        return "Directo";
      case "request":
        return "Solicitar";
      case "close":
        return "Cerrar";
      case "sector":
        return "Sector";
      case "parametrization":
        return "Parametrización";
      case "criteria":
        return "Criterio";
      case "findingparametrizationcriteria":
        return "Criterios Parametrización para Hallazgos"
      case "correctiveactionparametrization":
        return "Parametrización de Acciones Correctivas";
      case "edit":
        return "Editar";
      case "findingstates":
        return "Estados de Hallazgos";
      case "read":
        return "Leer";
      case "deactivate":
        return "Desactivar";
      case "activate":
        return "Activar";
      case "configure":
        return "Configuración";
      case "delete":
        return "Eliminar";
      case "findingtypes":
        return "Tipos de Hallazgos";
      case "modules":
        return "Modulos";
      case "views":
        return "Vistas";
      case "claims":
        return "Permisos";
      case "user":
        return "Usuario";
      case "findings":
        return "Hallazgos";
      case "administration":
        return "Administración";
      case "plant":
        return "Planta"
      case "plants":
        return "Plantas"
      case "company":
        return "Empresa"
      case "job":
        return "Puesto"
      case "reassignment":
        return "Reasignación"
      case "expirationdate":
        return "Fecha Vencimiento"
      case "readall":
        return "Leer Todos"
      case "editprofile":
        return "Editar Perfil"
      case "audittype":
        return "Tipo de Auditoría";
      case "correctiveactions":
        return "Acciones Correctivas"
      case "evaluate":
        return "Evaluar";
      case "duedate":
        return "Fecha de Vencimiento";
      case "extend":
        return "Extender"
      case "task":
        return "Tarea";
      case "planningduedateextention":
        return "Extender Fecha de Planificación";
      case "evaluateduedateextention":
        return "Extender Fecha de Evaluación";
      case "plannigduedate":
        return "Fecha de Planificación";
      case "evaluateduedate":
        return "Fecha de Evaluación";
      case "correctiveactionstate":
        return "Estados de Acciones Correctivas";
      case "fishbonecategory":
        return "Categorias de Ishikawa";
      case "tasks":
        return "Tareas"
      case "duedateextention":
        return "Extender fecha de Vencimiento"
      case "taskstate":
        return "Estados de Tareas"
      default:
        return word;
    }
  }

}
