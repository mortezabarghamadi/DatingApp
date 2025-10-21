import {Component, OnInit} from '@angular/core';
import { Observable } from 'rxjs';
import { UserDTO } from '../DTOs/UserDTO';
import {AccountService} from '../services/account.service';
import LoginDTO from "../DTOs/account/LoginDTO";
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit{

  model: LoginDTO  = new LoginDTO();
  currentUser$: Observable<UserDTO> | undefined;

  constructor(public accountService: AccountService,private router:Router,private toastr:ToastrService) {
  }

  ngOnInit(): void {
    this.currentUser$=this.accountService.currentUser;
  }

  login() {
    this.accountService.login(this.model).subscribe(data => {
      this.router.navigateByUrl('/members');
      console.log(data);
    }, error => {
      this.toastr.error(error.error);
    });
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

}
