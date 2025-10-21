import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { EditMemberComponent } from '../members/edit-member/edit-member.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard  {

  canDeactivate(component: EditMemberComponent): boolean {
  
    if(component.editForm.dirty){
      return confirm('آیا از انجام کار مطمئن هستید؟');
    }

    return true;
  }


}
