import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { ListsComponent } from './lists/lists.component';
import { MessageComponent } from './message/message.component';
import { AuthGuard } from './_guards/auth.guard';
import { EditMemberComponent } from './members/edit-member/edit-member.component';
import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';

const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'members', component: MemberListComponent, canActivate: [AuthGuard] },
    { path: 'members/:username', component: MemberDetailComponent },
    { path: 'member/edit', component: EditMemberComponent, canDeactivate: [PreventUnsavedChangesGuard] },
    { path: 'lists', component: ListsComponent },
    { path: 'messages', component: MessageComponent },
    { path: '**', component: HomeComponent, pathMatch: 'full' }
];

@NgModule({
    imports: [
        RouterModule.forRoot(appRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutingModule { }