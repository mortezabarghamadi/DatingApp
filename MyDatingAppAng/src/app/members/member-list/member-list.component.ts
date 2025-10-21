import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs';
import { UserDTO } from 'src/app/DTOs/UserDTO';
import { MemberDTO } from 'src/app/DTOs/member/MemberDTO';
import { Pagination } from 'src/app/DTOs/pagination';
import { UserParams } from 'src/app/DTOs/userParams';
import { AccountService } from 'src/app/services/account.service';
import { MemberService } from 'src/app/services/member.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})

export class MemberListComponent implements OnInit {
  members: MemberDTO[] | undefined;
  pagination: Pagination;
  userParams: UserParams;
  user: UserDTO;
  genderList = [{ value: 'male', display: 'مرد' }, { value: 'female', display: 'خانوم' }]

  constructor(private memberService: MemberService, private accountService: AccountService) {
    this.accountService.currentUser.pipe(take(1)).subscribe(user => {
      this.user = user;
      this.userParams = new UserParams(user);
    })
  }


  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers() {
    this.memberService.getMembers(this.userParams).subscribe(response => {
      this.members = response.result;
      this.pagination = response.pagination;
    })
  }

  resetFilters() {
    this.userParams = new UserParams(this.user);
    this.loadMembers();
  }

  pageChnaged(event: any) {
    this.userParams.pageNumber = event.page;
    this.loadMembers();
  }

}
